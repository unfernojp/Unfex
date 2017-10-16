/**
 * Unfex - unferno extentions
 * 
 * @overview	UX provides a basic set of classes that can be used as a foundation for application development.
 * @author		taka:unferno.jp
 * @version		1.0.0
 * @see			http://unferno.jp/
 * 
 * Licensed under the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Unfex.Commands;
using Unfex.Events;
using Unfex.Scenes;
using Unfex.Utils;
using UnityEngine;

namespace Unfex.Managers {

	public sealed class UXSceneManager:IEventDelegatable {

		#region Static Fields
		static UXSceneManager _instance = null;
		#endregion



		#region Properties
		public UXEventDelegate eventDelegate {
			get { return _eventDelegate; }
		}
		UXEventDelegate _eventDelegate = null;

		public bool debugMode {
			get { return _debugMode; }
			set { _debugMode = value; }
		}
		bool _debugMode = false;

		public SceneId startPosition {
			get { return _startPosition; }
		}
		SceneId _startPosition = null;

		public SceneId currentPosition {
			get { return _currentStatus.sceneId; }
		}

		public SceneId endPosition {
			get { return _endPosition; }
		}
		SceneId _endPosition = null;

		public object[] data {
			get { return _data; }
		}
		object[] _data = null;

		StageObject _stage = null;

		public UXSceneManager.State state {
			get { return _state; }
			private set {
				UXSceneManager.State oldValue = _state;

				_state = value;

				if ( oldValue != _state ) {
					UXEventDelegate.Execute( new UXEventData( this, DataEventData.onStateChanged, false ) );
				}
			}
		}
		UXSceneManager.State _state = UXSceneManager.State.Idling;
		#endregion



		#region Fields
		public enum State { Idling, Executing, Stopping, Erroring }

		List<SceneQuery> _queues = new List<SceneQuery>();

		bool _isQuerySequence = false;

		bool _historiable = true;
		int _historyPointer = 0;
		List<SceneId> _histories = new List<SceneId>();

		EditableSceneStatus _currentStatus = null;
		EditableSceneStatus _nextStatus = null;

		ParallelList _commandList = null;
		#endregion



		#region Constructors
		private UXSceneManager() {
			_eventDelegate = new UXEventDelegate( this );
			_eventDelegate.RegisterEvent( UXEventData.onStart );
			_eventDelegate.RegisterEvent( UXEventData.onProgress );
			_eventDelegate.RegisterEvent( UXEventData.onComplete );
			_eventDelegate.RegisterEvent( UXEventData.onStop );
			_eventDelegate.RegisterEvent( UXEventData.onError );

			_stage = new StageObject();

			_endPosition = SceneId.NaS;

			_currentStatus = new EditableSceneStatus();
			_currentStatus.scene = _stage;
			_currentStatus.sceneId = SceneId.NaS;

			_nextStatus = new EditableSceneStatus();
			_nextStatus.scene = _stage;
			_nextStatus.sceneId = _stage.sceneId;
		}
		#endregion



		#region Static Methods
		public static UXSceneManager GetInstance() {
			if ( _instance == null ) {
				_instance = new UXSceneManager();
			}

			return _instance;
		}
		#endregion



		#region Methods
		public bool Go( SceneObject sceneObject, object[] data = null, bool historiable = true ) {
			if ( sceneObject == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Go()", "sceneObject" ) ); }

			return Go( sceneObject.sceneId.path, data, historiable );
		}

		public bool Go( SceneId sceneId, object[] data = null, bool historiable = true ) {
			if ( sceneId == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Go()", "sceneId" ) ); }

			return Go( sceneId.path, data, historiable );
		}

		public bool Go( string scenePath, object[] data = null, bool historiable = true ) {
			switch ( _state ) {
				case UXSceneManager.State.Idling :
					_startPosition = new SceneId( _currentStatus.sceneId );
					break;
				case UXSceneManager.State.Executing :
				case UXSceneManager.State.Stopping :
				case UXSceneManager.State.Erroring :
					_queues.Add( new SceneQuery( scenePath, data, historiable ) );
					return true;
			}

			if ( _nextStatus.scene != null && _nextStatus.scene.parent != null ) {
				_currentStatus.slidePolicy = _nextStatus.scene.parent.slideChildren;
			} else {
				_currentStatus.slidePolicy = false;
			}

			_endPosition = _currentStatus.sceneId.Transfer( scenePath );

			_data = data;

			if ( _endPosition.Equals( SceneId.NaS ) ) { return false; }
			if ( _endPosition.Equals( _currentStatus.sceneId ) ) { return true; }

			if ( !_isQuerySequence ) {
				if ( _debugMode ) {
					Debug.Log( "[SceneManager] onStart endPosition=" + _endPosition.path );
				}

				if ( UXEventDelegate.Execute( new UXEventData( this, UXEventData.onStart, true ) ) ) {
					_historiable = historiable;
					state = UXSceneManager.State.Executing;

					_process();

					return true;
				}
			} else {
				_historiable = historiable;
				state = UXSceneManager.State.Executing;

				_process();
			}

			return false;
		}

		public bool Forward( bool historiable = false ) {
			_historiable = historiable;
			_historyPointer = Math.Min( _historyPointer + 1, _histories.Count - 1 );

			return Go( _histories[_historyPointer] );
		}

		public bool Back( bool historiable = false ) {
			_historiable = historiable;
			_historyPointer = Math.Max( 0, _historyPointer - 1 );

			return Go( _histories[_historyPointer] );
		}

		void _process() {
			_nextStatus = _getNextStatus( _endPosition, _currentStatus );

			if ( _nextStatus.eventType == UXEventData.onError ) { return; }

			_commandList = new ParallelList();

			UXEventDelegate.AddListener( _commandList, UXEventData.onComplete, _parallelListOnComplete );

			Dictionary<string, EditableSceneStatus> eventTypeStatus = new Dictionary<string, EditableSceneStatus>();

			if ( _nextStatus.phase == SceneEventData.Phase.Overlap ) {
				_currentStatus.phase = _nextStatus.phase;

				if ( _debugMode ) {
					Debug.Log( "[SceneManager] " + _currentStatus.sceneId.path + "\neventType=" + _currentStatus.eventType + " phase=" + _currentStatus.phase + " direction=" + _currentStatus.direction + " slidePolicy=" +_currentStatus.slidePolicy );
					Debug.Log( "[SceneManager] " + _nextStatus.sceneId.path + "\neventType=" + _nextStatus.eventType + " phase=" + _nextStatus.phase + " direction=" + _nextStatus.direction + " slidePolicy=" +_nextStatus.slidePolicy );
				}

				UXEventDelegate.Execute( new UXEventData( this, UXEventData.onProgress, false ) );

				eventTypeStatus.Add( StringUtil.ToPascal( _currentStatus.eventType ), _currentStatus );
				eventTypeStatus.Add( StringUtil.ToPascal( _currentStatus.eventType + _currentStatus.phase + _currentStatus.direction ), _currentStatus );

				eventTypeStatus.Add( StringUtil.ToPascal( _nextStatus.eventType ), _nextStatus );
				eventTypeStatus.Add( StringUtil.ToPascal( _nextStatus.eventType + _nextStatus.phase + _nextStatus.direction ), _nextStatus );

				_nextStatus.sceneId = _currentStatus.sceneId;
				_nextStatus.eventType = _currentStatus.eventType;
				_nextStatus.direction = _currentStatus.direction;
			} else {
				if ( _debugMode ) {
					Debug.Log( "[SceneManager] " + _nextStatus.sceneId.path + "\neventType=" + _nextStatus.eventType + " phase=" + _nextStatus.phase + " direction=" + _nextStatus.direction + " slidePolicy=" +_nextStatus.slidePolicy );
				}

				UXEventDelegate.Execute( new UXEventData( this, UXEventData.onProgress, false ) );

				eventTypeStatus.Add( StringUtil.ToPascal( _nextStatus.eventType ), _nextStatus );
				eventTypeStatus.Add( StringUtil.ToPascal( _nextStatus.eventType + _nextStatus.phase + _nextStatus.direction ), _nextStatus );
			}

			foreach ( KeyValuePair<string, EditableSceneStatus> pair in eventTypeStatus ) {
				MethodInfo methodInfo = pair.Value.scene.GetType().GetMethod( pair.Key );

				if ( methodInfo == null ) { continue; }

				switch ( pair.Key ) {
					case "OnEnter" :
					case "OnExit" :
						_commandList.Add( new CoroutineStart( pair.Value.scene, methodInfo, new object[]{ pair.Value.ToSceneStatus() } ) );
						break;
					default :
						_commandList.Add( new CoroutineStart( pair.Value.scene, methodInfo ) );
						break;
				}
			}

			_commandList.Execute();
		}

		void _parallelListOnComplete() {
			UXEventDelegate.RemoveListener( _commandList, UXEventData.onComplete, _parallelListOnComplete );

			_commandList = null;
			_currentStatus = _nextStatus;

			switch ( _currentStatus.eventType ) {
				case SceneEventData.onLeave :
				case SceneEventData.onExit :
				case SceneEventData.onEnter :
					_process();
					break;
				case SceneEventData.onArrive :
					if ( _currentStatus.sceneId.Equals( _endPosition ) ) {
						state = UXSceneManager.State.Idling;

						_isQuerySequence = false;

						while ( _queues.Count > 0 ) {
							SceneQuery queue = _queues[0];

							_queues.RemoveAt( 0 );

							if ( _currentStatus.sceneId.path == queue.scenePath ) { continue; }

							_isQuerySequence = true;

							Go( queue.scenePath, queue.data, queue.historiable );
							break;
						}

						if ( _isQuerySequence ) { return; }

						if ( _debugMode ) {
							Debug.Log( "[SceneManager] onComplete" );
						}

						if ( _historiable ) {
							_histories = _histories.GetRange( 0, Math.Min( _historyPointer + 1, _histories.Count ) );

							_historyPointer = _histories.Count;
							_histories.Add( _endPosition );
						}

						_historiable = true;

						UXEventDelegate.Execute( new UXEventData( this, UXEventData.onComplete, false ) );

						_data = null;
					} else {
						_process();
					}
					break;
				case SceneEventData.onError :
					UXEventDelegate.Execute( new ErrorEventData( this, UXEventData.onError, false, new Exception( "" ) ) );
					break;
			}
		}

		EditableSceneStatus _getNextStatus( SceneId sceneId, EditableSceneStatus status ) {
			SceneId currentSceneId = new SceneId( status.sceneId );
			SceneId nextSceneId = new SceneId( sceneId );

			SceneId lineSceneId = sceneId.GetRange( 0, currentSceneId.length );

			EditableSceneStatus currentStatus = new EditableSceneStatus( status );
			EditableSceneStatus nextStatus = new EditableSceneStatus( status );

			if ( currentStatus.eventType == SceneEventData.onLeave ) {
				nextStatus.eventType = SceneEventData.onExit;

				if ( currentSceneId.Contains( nextSceneId ) ) {
					nextStatus.direction = SceneEventData.Direction.Down;
				} else if ( currentStatus.slidePolicy && currentSceneId.Lines( lineSceneId ) ) {
					nextStatus.direction = SceneEventData.Direction.Slide;
				} else {
					nextStatus.direction = SceneEventData.Direction.Up;
				}
			} else if ( currentStatus.eventType == SceneEventData.onExit ) {
				nextStatus.eventType = SceneEventData.onEnter;

				if ( currentSceneId.Contains( nextSceneId ) ) {
					nextStatus.sceneId = nextSceneId.GetRange( 0, currentSceneId.length + 1 );
				} else if ( currentStatus.slidePolicy && currentSceneId.Lines( lineSceneId ) ) {
					SceneObject lineScene = Find( lineSceneId );
					SceneObject currentScene = Find( currentSceneId );

					if ( lineScene == null || currentScene == null ) {
						nextStatus.eventType = SceneEventData.onError;
						nextStatus.direction = SceneEventData.Direction.None;
					} else {
						nextStatus.sceneId = nextSceneId;
					}
				} else {
					nextStatus.sceneId = currentSceneId.Transfer( "../" );
				}
			} else if ( currentStatus.eventType == SceneEventData.onEnter ) {
				nextStatus.eventType = SceneEventData.onExit;

				switch ( currentStatus.direction ) {
					case SceneEventData.Direction.Down :
					case SceneEventData.Direction.Slide :
						if ( currentSceneId.Equals( nextSceneId ) ) {
							nextStatus.eventType = SceneEventData.onArrive;
							nextStatus.direction = SceneEventData.Direction.None;
						} else if ( currentSceneId.Contains( nextSceneId ) ) {
							nextStatus.direction = SceneEventData.Direction.Down;
						} else {
							nextStatus.direction = SceneEventData.Direction.Up;
						}

						break;
					case SceneEventData.Direction.Up :
						if ( currentSceneId.Equals( nextSceneId ) ) {
							nextStatus.eventType = SceneEventData.onArrive;
							nextStatus.direction = SceneEventData.Direction.None;
						}
						else if ( currentSceneId.Contains( nextSceneId ) ) {
							nextStatus.direction = SceneEventData.Direction.Down;
						}
						else if ( currentStatus.slidePolicy && currentSceneId.Lines( lineSceneId ) ) {
							nextStatus.direction = SceneEventData.Direction.Slide;
						}

						break;
				}
			} else if ( currentStatus.eventType == SceneEventData.onArrive || currentStatus.eventType == SceneEventData.onError ) {
				nextStatus.eventType = SceneEventData.onLeave;
				nextStatus.direction = SceneEventData.Direction.None;
			} else {
				nextStatus.sceneId = new SceneId( "/" );
				nextStatus.eventType = SceneEventData.onEnter;
				nextStatus.direction = SceneEventData.Direction.Down;
			}

			nextStatus.scene = Find( nextStatus.sceneId );

			if ( nextStatus.scene != null ) {
				if ( nextStatus.scene.parent != null ) {
					nextStatus.slidePolicy = nextStatus.scene.parent.slideChildren;
				} else {
					nextStatus.slidePolicy = false;
				}

				if ( currentStatus.eventType == SceneEventData.onExit && nextStatus.eventType == SceneEventData.onEnter && nextStatus.phase == SceneEventData.Phase.Part ) {
					nextStatus.phase = SceneEventData.Phase.Overlap;
				}
				else {
					nextStatus.phase = SceneEventData.Phase.Part;
				}

				return nextStatus;
			}

			nextStatus.eventType = SceneEventData.onError;
			nextStatus.direction = SceneEventData.Direction.None;

			return nextStatus;
		}

		public void Stop() {
			switch ( _state ) {
				case UXSceneManager.State.Idling :
					throw new Exception( "SceneManager that are not running can not be stopped." );
				case UXSceneManager.State.Executing :
					break;
				case UXSceneManager.State.Stopping :
				case UXSceneManager.State.Erroring :
					throw new Exception( "SceneManager in stopping process can not be stopped." );
			}

			state = UXSceneManager.State.Stopping;

			if ( _commandList != null ) {
				UXEventDelegate.RemoveListener( _commandList, UXEventData.onComplete, _parallelListOnComplete );

				_commandList.Cancel();
				_commandList = null;
			}

			UXEventDelegate.Execute( new UXEventData( this, UXEventData.onStop, true ) );

			state = UXSceneManager.State.Idling;
		}

		public SceneObject Add( SceneObject child ) {
			return _stage.Add( child );
		}

		public SceneObject Insert( int index, SceneObject child ) {
			return _stage.Insert( index, child );
		}

		public SceneObject Remove( SceneObject child ) {
			return _stage.Remove( child );
		}

		public SceneObject RemoveAt( int index ) {
			return _stage.RemoveAt( index );
		}

		public void RemoveAll() {
			_stage.RemoveAll();
		}

		public SceneObject GetChildByName( string name ) {
			return _stage.GetChildByName( name );
		}

		public SceneObject GetChildAt( int index ) {
			return _stage.GetChildAt( index );
		}

		public int GetChildIndex( SceneObject child ) {
			return _stage.GetChildIndex( child );
		}

		public void SwapChildren( SceneObject child1, SceneObject child2 ) {
			_stage.SwapChildren( child1, child2 );
		}

		public void SwapChildrenAt( int index1, int index2 ) {
			_stage.SwapChildrenAt( index1, index2 );
		}

		public SceneObject Find( SceneId sceneId ) {
			if ( sceneId == null ) { return null; }
			if ( sceneId == SceneId.NaS ) { return null; }

			SceneObject target = _stage;
			string[] segments = sceneId.path.Split( new string[]{ "/" }, StringSplitOptions.None );

			for ( int i = 1; i < segments.Length - 1; i++ ) {
				target = target.GetChildByName( segments[i] );
			}

			return target;
		}
		#endregion



		#region Classes
		public sealed class StageObject:SceneObject {

			#region Properties
			public override SceneObject root {
				get { return this; }
			}

			public override SceneObject parent {
				get { return null; }
			}
			#endregion



			#region Constructors
			public StageObject() {

			}
			#endregion
		}
		#endregion



		#region Classes
		private class SceneQuery {
			
			#region Properties
			public string scenePath {
				get { return _scenePath; }
			}
			string _scenePath = "";

			public object[] data {
				get { return _data; }
			}
			object[] _data = null;

			public bool historiable {
				get { return _historiable; }
			}
			bool _historiable = false;
			#endregion



			#region Constructors
			public SceneQuery( string scenePath, object[] data = null, bool historiable = true ) {
				_scenePath = scenePath;
				_data = data;
				_historiable = historiable;
			}
			#endregion
		}
		#endregion



		#region Classes
		private class EditableSceneStatus {

			#region Properties
			public SceneObject scene {
				get { return _scene; }
				set { _scene = value; }
			}
			SceneObject _scene = null;

			public SceneId sceneId {
				get { return _sceneId; }
				set { _sceneId = value; }
			}
			SceneId _sceneId = SceneId.NaS;

			public string eventType {
				get { return _eventType; }
				set { _eventType = value; }
			}
			string _eventType = "";

			public SceneEventData.Phase phase {
				get { return _phase; }
				set { _phase = value; }
			}
			SceneEventData.Phase _phase = SceneEventData.Phase.Part;

			public SceneEventData.Direction direction {
				get { return _direction; }
				set { _direction = value; }
			}
			SceneEventData.Direction _direction = SceneEventData.Direction.None;

			public bool slidePolicy {
				get { return _slidePolicy; }
				set { _slidePolicy = value; }
			}
			bool _slidePolicy = false;
			#endregion



			#region Constructors
			public EditableSceneStatus() {

			}

			public EditableSceneStatus( EditableSceneStatus status ) {
				if ( status != null ) {
					_scene = status.scene;
					_sceneId = new SceneId( status.sceneId );
					_eventType = status.eventType;
					_phase = status.phase;
					_direction = status.direction;
					_slidePolicy = status.slidePolicy;
				}
			}
			#endregion



			#region Methods
			public SceneStatus ToSceneStatus() {
				return new SceneStatus( _eventType, _phase, _direction, _slidePolicy );
			}
			#endregion
		}
		#endregion
	}
}
