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

using UnityEngine;
using UnityEngine.UI;
using Unfex.Events;

namespace Unfex.Components {

	[RequireComponent( typeof( Scrollbar ) )]
	public class UXScrollbar:UXComponent {

		#region Properties
		public Scrollbar scrollbar {
			get { return _scrollbar; }
		}
		Scrollbar _scrollbar = null;

		public bool interactable {
			get { return _scrollbar.interactable; }
			set { _scrollbar.interactable = value; }
		}

		public Scrollbar.Direction direction {
			get { return _scrollbar.direction; }
			set { _scrollbar.direction = value; }
		}

		public RectTransform handleRect {
			get { return _scrollbar.handleRect; }
			set { _scrollbar.handleRect = value; }
		}

		public int numberOfSteps {
			get { return _scrollbar.numberOfSteps; }
			set { _scrollbar.numberOfSteps = value; }
		}

		public float size {
			get { return _scrollbar.size; }
			set { _scrollbar.size = value; }
		}

		public float value {
			get { return _scrollbar.value; }
			set { _scrollbar.value = value; }
		}
		#endregion



		#region Methods
		public Selectable FindSelectableOnDown() {
			return _scrollbar.FindSelectableOnDown();
		}

		public Selectable FindSelectableOnLeft() {
			return _scrollbar.FindSelectableOnLeft();
		}

		public Selectable FindSelectableOnRight() {
			return _scrollbar.FindSelectableOnRight();
		}

		public Selectable FindSelectableOnUp() {
			return _scrollbar.FindSelectableOnUp();
		}

		public void GraphicUpdateComplete() {
			_scrollbar.GraphicUpdateComplete();
		}

		public void LayoutComplete() {
			_scrollbar.LayoutComplete();
		}

		public void Rebuild( CanvasUpdate executing ) {
			_scrollbar.Rebuild( executing );
		}

		public void SetDirection( Scrollbar.Direction direction, bool includeRectLayouts ) {
			_scrollbar.SetDirection( direction, includeRectLayouts );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_scrollbar = GetComponent<Scrollbar>();
			_scrollbar.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_scrollbar.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onValueChanged( float value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
