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

using System.Collections;
using Unfex.Components;
using Unfex.Events;
using Unfex.Views.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Unfex.Views {

	public class AlertDialog:DialogBase {

		#region Properties
		public Text messageText {
			get { return _messageText; }
		}
		[SerializeField] Text _messageText = null;

		public UXButton okButton {
			get { return _okButton; }
		}
		[SerializeField] UXButton _okButton = null;
		#endregion



		#region Static Methods
		public static AlertDialog GetPrefab() {
			GameObject gameObject = Resources.Load( "Unfex/Prefabs/Views/AlertDialog" ) as GameObject;

			AlertDialog prefab = gameObject.GetComponent<AlertDialog>();

			return prefab;
		}
		#endregion



		#region Methods
		public override IEnumerator Show() {
			yield return null;
		}

		public override IEnumerator Hide() {
			yield return null;
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();
		}

		protected virtual void Init( string message ) {
			base.Init();

			_messageText.text = message;
		}

		protected override void Start() {
			base.Start();

			rectTransform.localPosition = new Vector3( 0, 0, 0 );
			rectTransform.anchorMin = new Vector2( 0, 0 );
			rectTransform.anchorMax = new Vector2( 1, 1 );
			rectTransform.sizeDelta = new Vector2( 0, 0 );

			UXEventDelegate.AddListener( _okButton, InputEventData.onClick, _okButtonOnClick );
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			UXEventDelegate.RemoveListener( _okButton, InputEventData.onClick, _okButtonOnClick );
		}

		void _okButtonOnClick() {
			OK();
		}
		#endregion
	}
}
