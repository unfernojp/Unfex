/**
 * Unfex - unferno extentions
 * 
 * @overview	Unfex provides a basic set of classes that can be used as a foundation for application development.
 * @author		taka:unferno.jp
 * @version		1.0.0
 * @see			http://unferno.jp/
 * 
 * Licensed under the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unfex.Events {

	public class UXEventDelegate {

		#region Static Properties
		public static bool initialized {
			get { return _initialized; }
		}
		static bool _initialized = false;

		public static EventSystem eventSystem {
			get { return _eventSystem; }
		}
		static EventSystem _eventSystem = null;
		#endregion



		#region Static Fields
		static Dictionary<UnityEventBase, List<UnityAction>> _eventCallbacks = new Dictionary<UnityEventBase, List<UnityAction>>();
		static Dictionary<UnityAction, UXEventData> _callbackEventDatas = new Dictionary<UnityAction, UXEventData>();
		#endregion



		#region Properties
		public IEventDelegatable target {
			get { return _target; }
		}
		IEventDelegatable _target = null;
		#endregion



		#region Fields
		Dictionary<string, UnityEvent> _events = new Dictionary<string, UnityEvent>();
		#endregion



		#region Constructors
		public UXEventDelegate( IEventDelegatable target ) {
			_target = target;
		}
		#endregion



		#region Static Methods
		[RuntimeInitializeOnLoadMethod] public static void Initialize() {
			if ( _initialized ) { return; }

			_initialized = true;

			if ( GameObject.FindObjectOfType<EventSystem>() != null ) { return; }

			GameObject gameObject = Resources.Load( "Unfex/Prefabs/Events/EventSystem" ) as GameObject;

			EventSystem prefab = gameObject.GetComponent<EventSystem>();

			_eventSystem = UXBehaviour.Instantiate( prefab );
			_eventSystem.name = "EventSystem";

			UXBehaviour.DontDestroyOnLoad( _eventSystem );
		}

		public static void AddListener( object target, string name, UnityAction callback ) {
			if ( !_initialized ) { throw new Exception( string.Format( "{0} is not initialized.", "UXEventDelegate" ) ); }
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "AddListener()", "target" ) ); }

			UnityEventBase unityEventBase = _getUnityEventBase( target, name );

			if ( unityEventBase == null ) { throw new Exception( string.Format( "Does not exist {0} field of the corresponding UnityEvent type.", name ) ); }

			if ( !_eventCallbacks.ContainsKey( unityEventBase ) ) {
				_eventCallbacks.Add( unityEventBase, new List<UnityAction>() );
			}

			List<UnityAction> callbacks = _eventCallbacks[unityEventBase];

			if ( !callbacks.Contains( callback ) ) {
				callbacks.Add( callback );
			}

			UnityEvent unityEvent = unityEventBase as UnityEvent;

			if ( unityEvent != null ) {
				unityEvent.AddListener( callback );
			}
		}

		public static void RemoveListener( object target, string name, UnityAction callback ) {
			if ( !_initialized ) { throw new Exception( string.Format( "{0} is not initialized.", "UXEventDelegate" ) ); }
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "RemoveListener()", "target" ) ); }

			UnityEventBase unityEventBase = _getUnityEventBase( target, name );

			if ( unityEventBase == null ) { throw new Exception( string.Format( "Does not exist {0} field of the corresponding UnityEvent type.", name ) ); }

			if ( _eventCallbacks.ContainsKey( unityEventBase ) ) {
				List<UnityAction> callbacks = _eventCallbacks[unityEventBase];

				if ( callbacks.Contains( callback ) ) {
					callbacks.Remove( callback );

					if ( callbacks.Count == 0 ) {
						_eventCallbacks.Remove( unityEventBase );
					}
				}
			}

			UnityEvent unityEvent = unityEventBase as UnityEvent;

			if ( unityEvent != null ) {
				unityEvent.RemoveListener( callback );
			}
		}

		public static bool Execute( UXEventData eventData ) {
			if ( !_initialized ) { throw new Exception( string.Format( "{0} is not initialized.", "UXEventDelegate" ) ); }
			if ( eventData == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Execute()", "eventData" ) ); }

			UnityEventBase unityEventBase = _getUnityEventBase( eventData.target, eventData.name );

			if ( unityEventBase == null ) { return true; }

			if ( _eventCallbacks.ContainsKey( unityEventBase ) ) {
				List<UnityAction> callbacks = _eventCallbacks[unityEventBase];

				for ( int i = 0; i < callbacks.Count; i++ ) {
					UnityAction callback = callbacks[i] as UnityAction;

					if ( _callbackEventDatas.ContainsKey( callback ) ) {
						_callbackEventDatas[callback] = eventData;
					} else {
						_callbackEventDatas.Add( callback, eventData );
					}
				}
			}

			UnityEvent unityEvent = unityEventBase as UnityEvent;

			if ( unityEvent != null ) {
				unityEvent.Invoke();
			}

			if ( _eventCallbacks.ContainsKey( unityEventBase ) ) {
				List<UnityAction> callbacks = _eventCallbacks[unityEventBase];

				for ( int i = 0; i < callbacks.Count; i++ ) {
					_callbackEventDatas.Remove( callbacks[i] );
				}
			}

			if ( eventData.IsDefaultPrevented() ) { return false; }

			return true;
		}

		public static UXEventData GetEventData( UnityAction callback ) {
			if ( !_initialized ) { throw new Exception( string.Format( "{0} is not initialized.", "UXEventDelegate" ) ); }
			if ( callback == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetEventData()", "callback" ) ); }

			if ( _callbackEventDatas.ContainsKey( callback ) ) { return _callbackEventDatas[callback]; }

			return null;
		}

		static UnityEventBase _getUnityEventBase( object target, string name ) {
			IEventDelegatable eventDelegatable = target as IEventDelegatable;

			if ( eventDelegatable != null && eventDelegatable.eventDelegate != null && eventDelegatable.eventDelegate._events.ContainsKey( name ) ) { return eventDelegatable.eventDelegate._events[name]; }

			Button button = target as Button;
			Dropdown dropdown = target as Dropdown;
			InputField inputField = target as InputField;
			Scrollbar scrollbar = target as Scrollbar;
			ScrollRect scrollRect = target as ScrollRect;
			Slider slider = target as Slider;
			Toggle toggle = target as Toggle;

			if ( button != null ) {
				switch ( name ) {
					case InputEventData.onClick : { return button.onClick; }
				}
			} else if ( dropdown != null ) {
				switch ( name ) {
					case DataEventData.onValueChanged : { return dropdown.onValueChanged; }
				}
			} else if ( inputField != null ) {
				switch ( name ) {
					case DataEventData.onEndEdit : { return inputField.onEndEdit; }
					case DataEventData.onValueChanged : { return inputField.onValueChanged; }
				}
			} else if ( scrollbar != null ) {
				switch ( name ) {
					case DataEventData.onValueChanged : { return scrollbar.onValueChanged; }
				}
			} else if ( scrollRect != null ) {
				switch ( name ) {
					case DataEventData.onValueChanged : { return scrollRect.onValueChanged; }
				}
			} else if ( slider != null ) {
				switch ( name ) {
					case DataEventData.onValueChanged : { return slider.onValueChanged; }
				}
			} else if ( toggle != null ) {
				switch ( name ) {
					case DataEventData.onValueChanged : { return toggle.onValueChanged; }
				}
			}

			return null;
		}
		#endregion



		#region Methods
		public void RegisterEvent( string name, UnityEvent unityEvent = null ) {
			if ( unityEvent == null ) {
				unityEvent = new UXEvent();
			}

			if ( _events.ContainsKey( name ) ) { throw new Exception( "" ); }

			_events[name] = unityEvent;
		}
		#endregion
	}
}
