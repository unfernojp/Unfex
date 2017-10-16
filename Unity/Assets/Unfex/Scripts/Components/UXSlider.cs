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

	[RequireComponent( typeof( Slider ) )]
	public class UXSlider:UXComponent {

		#region Properties
		public Slider slider {
			get { return _slider; }
		}
		Slider _slider = null;

		public bool interactable {
			get { return _slider.interactable; }
			set { _slider.interactable = value; }
		}

		public Slider.Direction direction {
			get { return _slider.direction; }
			set { _slider.direction = value; }
		}

		public RectTransform fillRect {
			get { return _slider.fillRect; }
			set { _slider.fillRect = value; }
		}

		public RectTransform handleRect {
			get { return _slider.handleRect; }
			set { _slider.handleRect = value; }
		}

		public float maxValue {
			get { return _slider.maxValue; }
			set { _slider.maxValue = value; }
		}

		public float minValue {
			get { return _slider.minValue; }
			set { _slider.minValue = value; }
		}

		public float normalizedValue {
			get { return _slider.normalizedValue; }
			set { _slider.normalizedValue = value; }
		}

		public float value {
			get { return _slider.value; }
			set { _slider.value = value; }
		}

		public bool wholeNumbers {
			get { return _slider.wholeNumbers; }
			set { _slider.wholeNumbers = value; }
		}
		#endregion



		#region Methods
		public Selectable FindSelectableOnDown() {
			return _slider.FindSelectableOnDown();
		}

		public Selectable FindSelectableOnLeft() {
			return _slider.FindSelectableOnLeft();
		}

		public Selectable FindSelectableOnRight() {
			return _slider.FindSelectableOnRight();
		}

		public Selectable FindSelectableOnUp() {
			return _slider.FindSelectableOnUp();
		}

		public void GraphicUpdateComplete() {
			_slider.GraphicUpdateComplete();
		}

		public void LayoutComplete() {
			_slider.LayoutComplete();
		}

		public void Rebuild( CanvasUpdate executing ) {
			_slider.Rebuild( executing );
		}

		public void SetDirection( Slider.Direction direction, bool includeRectLayouts ) {
			_slider.SetDirection( direction, includeRectLayouts );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_slider = GetComponent<Slider>();
			_slider.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_slider.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onValueChanged( float value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
