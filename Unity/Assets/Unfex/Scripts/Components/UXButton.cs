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

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unfex.Events;

namespace Unfex.Components {

	[RequireComponent( typeof( Button ) )]
	public class UXButton:UXComponent, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

		#region Properties
		public Image image {
			get { return _image; }
		}
		Image _image = null;

		public Button button {
			get { return _button; }
		}
		Button _button = null;

		public bool interactable {
			get { return _button.interactable; }
			set { _button.interactable = value; }
		}

		public bool isPointerDown {
			get { return _isPointerDown; }
		}
		bool _isPointerDown = false;

		public bool isPointerLeftDown {
			get { return _isPointerLeftDown; }
		}
		bool _isPointerLeftDown = false;

		public bool isPointerRightDown {
			get { return _isPointerRightDown; }
		}
		bool _isPointerRightDown = false;

		public bool isPointerEnter {
			get { return _isPointerEnter; }
		}
		bool _isPointerEnter = false;
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( InputEventData.onMouse1Down );
			eventDelegate.RegisterEvent( InputEventData.onMouse2Down );
			eventDelegate.RegisterEvent( InputEventData.onMouse1Up );
			eventDelegate.RegisterEvent( InputEventData.onMouse2Up );
			eventDelegate.RegisterEvent( InputEventData.onMouseEnter );
			eventDelegate.RegisterEvent( InputEventData.onMouseExit );
			eventDelegate.RegisterEvent( InputEventData.onTap );
			eventDelegate.RegisterEvent( InputEventData.onClick );

			_image = GetComponent<Image>();
			_button = GetComponent<Button>();
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			if ( _image != null ) {
				_image.sprite = null;
			}

			_button.spriteState = default( SpriteState );
		}

		public virtual void OnPointerDown( PointerEventData eventData ) {
			_isPointerDown = true;
			_isPointerLeftDown = false;
			_isPointerRightDown = false;

			if ( !button.enabled ) { return; }
			if ( !button.interactable ) { return; }

			if ( eventData.button == PointerEventData.InputButton.Left ) {
				_isPointerLeftDown = true;

				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouse1Down, false, eventData, eventData.position ) );
			}

			if ( eventData.button == PointerEventData.InputButton.Right ) {
				_isPointerRightDown = true;

				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouse2Down, false, eventData, eventData.position ) );
			}
		}

		public virtual void OnPointerUp( PointerEventData eventData ) {
			_isPointerDown = false;
			_isPointerLeftDown = false;
			_isPointerRightDown = false;

			if ( !button.enabled ) { return; }
			if ( !button.interactable ) { return; }

			if ( eventData.button == PointerEventData.InputButton.Left ) {
				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouse1Up, false, eventData, eventData.position ) );
			}

			if ( eventData.button == PointerEventData.InputButton.Right ) {
				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouse2Up, false, eventData, eventData.position ) );
			}
		}

		public virtual void OnPointerEnter( PointerEventData eventData ) {
			_isPointerEnter = true;

			UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouseEnter, false, eventData, eventData.position ) );
		}

		public virtual void OnPointerExit( PointerEventData eventData ) {
			_isPointerEnter = false;

			UXEventDelegate.Execute( new InputEventData( this, InputEventData.onMouseExit, false, eventData, eventData.position ) );
		}

		public virtual void OnPointerClick( PointerEventData eventData ) {
			if ( !button.enabled ) { return; }
			if ( !button.interactable ) { return; }

			if ( eventData.button == PointerEventData.InputButton.Left ) {
				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onTap, false, eventData, eventData.position ) );
				UXEventDelegate.Execute( new InputEventData( this, InputEventData.onClick, false, eventData, eventData.position ) );
			}
		}
		#endregion
	}
}
