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
using UnityEngine.UI;
using Unfex.Events;

namespace Unfex.Components {

	[RequireComponent( typeof( Toggle ) )]
	public class UXToggle:UXComponent {

		#region Properties
		public Toggle toggle {
			get { return _toggle; }
		}
		Toggle _toggle = null;

		public bool interactable {
			get { return _toggle.interactable; }
			set { _toggle.interactable = value; }
		}

		public Graphic graphic {
			get { return _toggle.graphic; }
			set { _toggle.graphic = value; }
		}

		public ToggleGroup group {
			get { return _toggle.group; }
			set { _toggle.group = value; }
		}

		public bool isOn {
			get { return _toggle.isOn; }
			set { _toggle.isOn = value; }
		}

		public Toggle.ToggleTransition toggleTransition {
			get { return _toggle.toggleTransition; }
			set { _toggle.toggleTransition = value; }
		}
		#endregion



		#region Methods
		public void GraphicUpdateComplete() {
			_toggle.GraphicUpdateComplete();
		}

		public void LayoutComplete() {
			_toggle.LayoutComplete();
		}

		public void Rebuild( CanvasUpdate executing ) {
			_toggle.Rebuild( executing );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_toggle = GetComponent<Toggle>();
			_toggle.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_toggle.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onValueChanged( bool value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
