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
using Unfex.Views.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Unfex.Views {

	public abstract class TextProgressIndicator:ProgressIndicatorBase {

		#region Properties
		public Text indicatorText {
			get { return _indicatorText; }
		}
		Text _indicatorText = null;
		#endregion




		#region Methods
		public override IEnumerator Show() {
			yield break;
		}

		public override void Update( float current, float total ) {

		}

		public override IEnumerator Hide() {
			yield break;
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();
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

			_indicatorText = gameObject.AddComponent<Text>();
			_indicatorText.text = "NOW LOADING ...";
			_indicatorText.fontSize = 30;
			_indicatorText.alignment = TextAnchor.MiddleCenter;
		}

		protected override void OnDestroy() {
			base.OnDestroy();
		}
		#endregion
	}
}
