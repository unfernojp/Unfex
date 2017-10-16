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

using System.Collections;
using Unfex.Managers;
using Unfex.Views.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Unfex.Views {

	public class TextPage:PageBase {

		#region Properties
		public Text messageText {
			get { return _messageText; }
		}
		[SerializeField] Text _messageText = null;
		#endregion



		#region Static Methods
		public static TextPage GetPrefab() {
			GameObject gameObject = Resources.Load( "Unfex/Prefabs/Views/TextPage" ) as GameObject;

			TextPage prefab = gameObject.GetComponent<TextPage>();

			return prefab;
		}
		#endregion



		#region Methods
		public override IEnumerator Show() {
			yield return UXViewManager.transition.FadeIn();
		}

		public override IEnumerator Hide() {
			yield return UXViewManager.transition.FadeOut();
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			GameObject messageTextObject = new GameObject( "MessageText" );
			messageTextObject.transform.SetParent( transform );

			_messageText = messageTextObject.AddComponent<Text>();
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();

			rectTransform.localPosition = new Vector3( 0, 0, 0 );
			rectTransform.anchorMin = new Vector2( 0, 0 );
			rectTransform.anchorMax = new Vector2( 1, 1 );
			rectTransform.sizeDelta = new Vector2( 0, 0 );
		}

		public override void OnFocus() {
			base.OnFocus();
		}

		public override void OnBlur() {
			base.OnBlur();
		}

		protected override void OnDestroy() {
			base.OnDestroy();
		}
		#endregion
	}
}
