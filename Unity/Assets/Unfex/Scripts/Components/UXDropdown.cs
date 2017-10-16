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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unfex.Events;

namespace Unfex.Components {

	[RequireComponent( typeof( Dropdown ) )]
	public class UXDropdown:UXComponent {

		#region Properties
		public Dropdown dropdown {
			get { return _dropdown; }
		}
		Dropdown _dropdown = null;

		public bool interactable {
			get { return _dropdown.interactable; }
			set { _dropdown.interactable = value; }
		}

		public Image captionImage {
			get { return _dropdown.captionImage; }
			set { _dropdown.captionImage = value; }
		}

		public Text captionText {
			get { return _dropdown.captionText; }
			set { _dropdown.captionText = value; }
		}

		public Image itemImage {
			get { return _dropdown.itemImage; }
			set { _dropdown.itemImage = value; }
		}

		public Text itemText {
			get { return _dropdown.itemText; }
			set { _dropdown.itemText = value; }
		}

		public List<Dropdown.OptionData> options {
			get { return _dropdown.options; }
			set { _dropdown.options = value; }
		}

		public RectTransform template {
			get { return _dropdown.template; }
			set { _dropdown.template = value; }
		}

		public int value {
			get { return _dropdown.value; }
			set { _dropdown.value = value; }
		}
		#endregion



		#region Methods
		public void AddOptions( List<string> options ) {
			_dropdown.AddOptions( options );
		}

		public void AddOptions( List<Sprite> options ) {
			_dropdown.AddOptions( options );
		}

		public void AddOptions( List<Dropdown.OptionData> options ) {
			_dropdown.AddOptions( options );
		}

		public void ClearOptions() {
			_dropdown.ClearOptions();
		}

		public void Hide() {
			_dropdown.Hide();
		}

		public void RefreshShownValue() {
			_dropdown.RefreshShownValue();
		}

		public void Show() {
			_dropdown.Show();
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_dropdown = GetComponent<Dropdown>();
			_dropdown.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_dropdown.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onValueChanged( int value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
