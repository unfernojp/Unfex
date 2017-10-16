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
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unfex.Commands;
using Unfex.Commands.DOTweens;
using Unfex.Events;
using Unfex.Views;
using Unfex.Views.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Unfex.Managers {

	public class UXViewManager {

		#region Static Properties
		public static bool initialized {
			get { return _initialized; }
		}
		static bool _initialized = false;

		public static CanvasScaler.ScaleMode uiScaleMode {
			get { return _uiScaleMode; }
			set {
				_uiScaleMode = value;

				_page.canvasScaler.uiScaleMode = _uiScaleMode;
				_dialog.canvasScaler.uiScaleMode = _uiScaleMode;
				_transition.canvasScaler.uiScaleMode = _uiScaleMode;
			}
		}
		static CanvasScaler.ScaleMode _uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

		public static Vector2 referenceResolution {
			get { return _referenceResolution; }
			set {
				_referenceResolution = value;

				_page.canvasScaler.referenceResolution = _referenceResolution;
				_dialog.canvasScaler.referenceResolution = _referenceResolution;
				_transition.canvasScaler.referenceResolution = _referenceResolution;
			}
		}
		static Vector2 _referenceResolution = Vector2.zero;

		public static CanvasScaler.ScreenMatchMode screenMatchMode {
			get { return _screenMatchMode; }
			set {
				_screenMatchMode = value;

				_page.canvasScaler.screenMatchMode = _screenMatchMode;
				_dialog.canvasScaler.screenMatchMode = _screenMatchMode;
				_transition.canvasScaler.screenMatchMode = _screenMatchMode;
			}
		}
		static CanvasScaler.ScreenMatchMode _screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

		public static Camera camera {
			get { return _camera; }
		}
		static Camera _camera = null;

		public static PageManager page {
			get { return _page; }
		}
		static PageManager _page = null;

		public static DialogManager dialog {
			get { return _dialog; }
		}
		static DialogManager _dialog = null;

		public static TransitionManager transition {
			get { return _transition; }
		}
		static TransitionManager _transition = null;
		#endregion



		#region Static Fields
		static GameObject _viewManagerObject = null;

		static GameObject _cameraObject = null;
		#endregion



		#region Constructors
		private UXViewManager() {
			
		}
		#endregion



		#region Static Methods
		public static void Initialize() {
			if ( _initialized ) { return; }

			_initialized = true;

			_viewManagerObject = new GameObject( "ViewManager" );

			UXBehaviour.DontDestroyOnLoad( _viewManagerObject );

			_cameraObject = new GameObject( "Camera" );
			_cameraObject.transform.SetParent( _viewManagerObject.transform );

			_camera = _cameraObject.AddComponent<Camera>();

			_page = new PageManager();
			_page.canvas.sortingOrder = 1;

			_dialog = new DialogManager();
			_dialog.canvas.sortingOrder = 2;

			_transition = new TransitionManager();
			_transition.canvas.sortingOrder = 3;
		}
		#endregion



		#region Classes
		public class PageManager {

			#region Properties
			public Canvas canvas {
				get { return _canvas; }
			}
			Canvas _canvas = null;

			public CanvasScaler canvasScaler {
				get { return _canvasScaler; }
			}
			CanvasScaler _canvasScaler = null;

			public GraphicRaycaster graphicRaycaster {
				get { return _graphicRaycaster; }
			}
			GraphicRaycaster _graphicRaycaster = null;

			public PageBase page {
				get { return _page; }
			}
			PageBase _page = null;
			#endregion



			#region Fields
			GameObject _pageManagerObject = null;

			IEnumerator _routineReplace = null;
			#endregion



			#region Constructors
			public PageManager() {
				_pageManagerObject = new GameObject( "PageManager" );
				_pageManagerObject.transform.SetParent( _viewManagerObject.transform );

				_canvas = _pageManagerObject.AddComponent<Canvas>();
				_canvas.renderMode = RenderMode.ScreenSpaceCamera;
				_canvas.worldCamera = _camera;

				_canvasScaler = _pageManagerObject.AddComponent<CanvasScaler>();

				_graphicRaycaster = _pageManagerObject.AddComponent<GraphicRaycaster>();
			}
			#endregion



			#region Methods
			public Coroutine Replace( PageBase prefab ) {
				return Replace( prefab, new object[]{} );
			}

			public Coroutine Replace( PageBase prefab, object[] args ) {
				_routineReplace = _replace( prefab, args );

				return UXBehaviour.StartCoroutine( _routineReplace );
			}

			IEnumerator _replace( PageBase prefab, object[] args ) {
				if ( _page != null ) {
					yield return _dialog.Close( true );

					yield return UXBehaviour.StartCoroutine( _page.Hide() );

					UXEventDelegate.Execute( new ViewEventData( _page, ViewEventData.onHide, false ) );

					UXBehaviour.Destroy( _page.gameObject );

					_page = null;

					GC.Collect();
					Resources.UnloadUnusedAssets();
				}

				if ( prefab != null ) {
					_page = UXBehaviour.InstantiateTo( prefab, _canvas.gameObject, args ) as PageBase;
					_page.name = prefab.name;
				}

				if ( _page != null ) {
					yield return null;

					yield return UXBehaviour.StartCoroutine( _page.Show() );

					UXEventDelegate.Execute( new ViewEventData( _page, ViewEventData.onShow, false ) );
				}
			}
			#endregion
		}
		#endregion



		#region Classes
		public class DialogManager:IEventDelegatable {

			#region Properties
			public UXEventDelegate eventDelegate {
				get { return _eventDelegate; }
			}
			UXEventDelegate _eventDelegate = null;

			public Canvas canvas {
				get { return _canvas; }
			}
			Canvas _canvas = null;

			public CanvasScaler canvasScaler {
				get { return _canvasScaler; }
			}
			CanvasScaler _canvasScaler = null;

			public GraphicRaycaster graphicRaycaster {
				get { return _graphicRaycaster; }
			}
			GraphicRaycaster _graphicRaycaster = null;

			public DataEventData result {
				get { return _result; }
			}
			DataEventData _result = null;
			#endregion



			#region Fields
			GameObject _dialogManagerObject = null;

			DialogBase _alertDialogPrefab = null;
			DialogBase _confirmDialogPrefab = null;
			DialogBase _promptDialogPrefab = null;

			List<DialogBase> _dialogs = null;

			bool _isPreventDefault = false;

			int _openingProcessCount = 0;
			int _closingProcessCount = 0;
			#endregion



			#region Constructors
			public DialogManager() {
				_dialogManagerObject = new GameObject( "DialogManager" );
				_dialogManagerObject.transform.SetParent( _viewManagerObject.transform );

				_canvas = _dialogManagerObject.AddComponent<Canvas>();
				_canvas.renderMode = RenderMode.ScreenSpaceCamera;
				_canvas.worldCamera = _camera;

				_canvasScaler = _dialogManagerObject.AddComponent<CanvasScaler>();

				_graphicRaycaster = _dialogManagerObject.AddComponent<GraphicRaycaster>();

				_dialogs = new List<DialogBase>();

				_eventDelegate = new UXEventDelegate( this );
				_eventDelegate.RegisterEvent( UXEventData.onOpen );
				_eventDelegate.RegisterEvent( UXEventData.onOpened );
				_eventDelegate.RegisterEvent( UXEventData.onClose );
				_eventDelegate.RegisterEvent( UXEventData.onClosed );
				_eventDelegate.RegisterEvent( UXEventData.onOK );
				_eventDelegate.RegisterEvent( UXEventData.onCancel );
				_eventDelegate.RegisterEvent( UXEventData.onInvoke );
			}
			#endregion



			#region Methods
			public void RegisterAlertDialog( DialogBase prefab ) {
				_alertDialogPrefab = prefab;
			}

			public void RegisterConfirmDialog( DialogBase prefab ) {
				_confirmDialogPrefab = prefab;
			}

			public void RegisterPromptDialog( DialogBase prefab ) {
				_promptDialogPrefab = prefab;
			}

			public Coroutine Alert( string message ) {
				DialogBase prefab = _alertDialogPrefab;

				if ( prefab == null ) {
					prefab = AlertDialog.GetPrefab();
				}

				return Open( prefab, new object[]{ message } );
			}

			public Coroutine Confirm( string message ) {
				DialogBase prefab = _confirmDialogPrefab;

				if ( prefab == null ) {
					prefab = AlertDialog.GetPrefab();
				}

				return Open( prefab, new object[]{ message } );
			}

			public Coroutine Prompt( string message, string defaultValue = "" ) {
				DialogBase prefab = _promptDialogPrefab;

				if ( prefab == null ) {
					prefab = PromptDialog.GetPrefab();
				}

				return Open( prefab, new object[]{ message, defaultValue } );
			}

			public Coroutine Open( DialogBase prefab, bool closeAll = false ) {
				return Open( prefab, null, closeAll );
			}

			public Coroutine Open( DialogBase prefab, object[] args, bool closeAll = false ) {
				if ( prefab == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Open()", "prefab" ) ); }

				return UXBehaviour.StartCoroutine( _open( prefab, args, closeAll ) );
			}

			IEnumerator _open( DialogBase prefab, object[] args, bool closeAll ) {
				while ( _openingProcessCount > 0 || _closingProcessCount > 0 ) {
					yield return null;
				}

				_result = null;
				_isPreventDefault = false;

				if ( !UXEventDelegate.Execute( new UXEventData( this, UXEventData.onOpen, true ) ) ) { yield break; }

				_openingProcessCount++;

				if ( closeAll ) {
					yield return UXBehaviour.StartCoroutine( _close( true, closeAll ) );
				}

				if ( _isPreventDefault ) {
					_openingProcessCount--;

					yield break;
				}

				DialogBase instance = UXBehaviour.InstantiateTo( prefab, _canvas.gameObject, args ) as DialogBase;
				instance.name = prefab.name;

				_dialogs.Add( instance );

				yield return UXBehaviour.StartCoroutine( instance.Show() );

				if ( _dialogs.Contains( instance ) ) {
					UXEventDelegate.AddListener( instance, UXEventData.onOK, _dialogOnOKCancel );
					UXEventDelegate.AddListener( instance, UXEventData.onCancel, _dialogOnOKCancel );

					UXEventDelegate.Execute( new UXEventData( this, UXEventData.onOpened, false ) );
				}

				_openingProcessCount--;

				while ( _dialogs.Contains( instance ) ) {
					yield return null;
				}
			}

			public Coroutine Close( bool closeAll = false ) {
				return UXBehaviour.StartCoroutine( _close( false, closeAll ) );
			}

			IEnumerator _close( bool internalCall, bool closeAll ) {
				_isPreventDefault = false;

				if ( !UXEventDelegate.Execute( new UXEventData( this, UXEventData.onClose, true ) ) ) {
					_isPreventDefault = true;

					yield break;
				}

				_closingProcessCount++;

				List<DialogBase> uncloseDialogs = null;
				List<DialogBase> closeDialogs = null;

				if ( closeAll ) {
					uncloseDialogs = new List<DialogBase>();
					closeDialogs = new List<DialogBase>( _dialogs );
				} else if ( _dialogs.Count > 0 ) {
					uncloseDialogs = _dialogs.GetRange( 0, _dialogs.Count - 1 );
					closeDialogs = _dialogs.GetRange( _dialogs.Count - 1, 1 );
				}

				if ( !internalCall ) {
					UXEventDelegate.Execute( new UXEventData( this, DataEventData.onCountChanged, false ) );
				}

				CommandList list = new ParallelList();

				for ( int i = 0; i < closeDialogs.Count; i++ ) {
					DialogBase dialog = closeDialogs[i];

					UXEventDelegate.RemoveListener( dialog, UXEventData.onOK, _dialogOnOKCancel );
					UXEventDelegate.RemoveListener( dialog, UXEventData.onCancel, _dialogOnOKCancel );

					dialog.container.DOKill();

					list.Add( new CoroutineStart( dialog, "Hide" ) );
				}

				yield return list.Execute();

				for ( int i = 0; i < closeDialogs.Count; i++ ) {
					DialogBase dialog = closeDialogs[i];

					if ( dialog != null ) {
						UXBehaviour.Destroy( dialog.gameObject );
					}
				}

				_dialogs = uncloseDialogs;

				Resources.UnloadUnusedAssets();

				UXEventDelegate.Execute( new UXEventData( this, UXEventData.onClosed, false ) );

				_closingProcessCount--;
			}
			#endregion



			#region EventHandlers
			void _dialogOnOKCancel() {
				DataEventData eventData = UXEventDelegate.GetEventData( _dialogOnOKCancel ) as DataEventData;

				DialogBase dialog = eventData.target as DialogBase;

				UXEventDelegate.RemoveListener( dialog, UXEventData.onOK, _dialogOnOKCancel );
				UXEventDelegate.RemoveListener( dialog, UXEventData.onCancel, _dialogOnOKCancel );

				_result = new DataEventData( this, eventData.name, true, eventData.data );

				if ( UXEventDelegate.Execute( _result ) ) {
					Close();
				}
			}
			#endregion
		}
		#endregion



		#region Classes
		public class TransitionManager {

			#region Properties
			public Canvas canvas {
				get { return _canvas; }
			}
			Canvas _canvas = null;

			public CanvasScaler canvasScaler {
				get { return _canvasScaler; }
			}
			CanvasScaler _canvasScaler = null;

			public GraphicRaycaster graphicRaycaster {
				get { return _graphicRaycaster; }
			}
			GraphicRaycaster _graphicRaycaster = null;
			#endregion



			#region Fields
			GameObject _transitionManagerObject = null;

			Image _blockade = null;
			Color _color = new Color( 0, 0, 0, 1 );

			IEnumerator _fadeRoutine = null;
			#endregion



			#region Constructors
			public TransitionManager() {
				_transitionManagerObject = new GameObject( "TransitionManager" );
				_transitionManagerObject.transform.SetParent( _viewManagerObject.transform );

				_canvas = _transitionManagerObject.AddComponent<Canvas>();
				_canvas.renderMode = RenderMode.ScreenSpaceCamera;
				_canvas.worldCamera = _camera;

				_canvasScaler = _transitionManagerObject.AddComponent<CanvasScaler>();

				_graphicRaycaster = _transitionManagerObject.AddComponent<GraphicRaycaster>();

				GameObject blockadeObject = new GameObject( "Blockade" );
				blockadeObject.transform.SetParent( _transitionManagerObject.transform );

				_blockade = blockadeObject.AddComponent<Image>();
				_blockade.color = _color;
				_blockade.rectTransform.localPosition = new Vector3( 0, 0, 0 );
				_blockade.rectTransform.anchorMin = new Vector2( 0, 0 );
				_blockade.rectTransform.anchorMax = new Vector2( 1, 1 );
				_blockade.rectTransform.sizeDelta = new Vector2( 0, 0 );
			}
			#endregion



			#region Methods
			public Coroutine ShowProgressIndicator() {
				return ShowProgressIndicator( null );
			}

			public Coroutine ShowProgressIndicator( ProgressIndicatorBase prefab ) {
				return null;
			}

			public Coroutine HideProgressIndicator() {
				return null;
			}

			public void SetFadeColor( Color color ) {
				_color = color;

				if ( _fadeRoutine == null ) {
					_blockade.color = new Color( _color.r, _color.g, _color.b, _blockade.color.a );
				}
			}

			public Coroutine FadeIn( float time = 0.5f ) {
				if ( _fadeRoutine != null ) {
					UXBehaviour.StopCoroutine( _fadeRoutine );

					_fadeRoutine = null;
				}

				SetFadeColor( _color );

				_fadeRoutine = _fadeIn( time );

				return UXBehaviour.StartCoroutine( _fadeRoutine );
			}

			IEnumerator _fadeIn( float time ) {
				if ( _blockade.color.a == 0 ) { yield break; }

				time = Math.Max( 0, time );

				yield return new DOFadeTo( _blockade, time, 0, false, Ease.Linear ).Execute();

				_blockade.raycastTarget = false;
			}

			public Coroutine FadeOut( float time = 0.5f ) {
				if ( _fadeRoutine != null ) {
					UXBehaviour.StopCoroutine( _fadeRoutine );

					_fadeRoutine = null;
				}

				SetFadeColor( _color );

				_fadeRoutine = _fadeOut( time );

				return UXBehaviour.StartCoroutine( _fadeRoutine );
			}

			IEnumerator _fadeOut( float time ) {
				if ( _blockade.color.a == 1 ) { yield break; }

				_blockade.raycastTarget = true;

				time = Math.Max( 0, time );

				yield return new DOFadeTo( _blockade, time, 1, false, Ease.Linear ).Execute();
			}
			#endregion
		}
		#endregion
	}
}
