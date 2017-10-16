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

namespace Unfex.Components {

	[RequireComponent( typeof( RawImage ) )]
	public class UXRawImage:UXComponent {

		#region Properties
		public RawImage rawImage {
			get { return _rawImage; }
		}
		RawImage _rawImage = null;

		public float alpha {
			get { return _rawImage.color.a; }
			set { _rawImage.color = new Color( _rawImage.color.r, _rawImage.color.g, _rawImage.color.b, value ); }
		}

		public Texture mainTexture {
			get { return _rawImage.mainTexture; }
		}

		public Texture texture {
			get { return _rawImage.texture; }
			set { _rawImage.texture = value; }
		}

		public Rect uvRect {
			get { return _rawImage.uvRect; }
			set { _rawImage.uvRect = value; }
		}
		#endregion



		#region Methods
		public void SetNativeSize() {
			_rawImage.SetNativeSize();
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			_rawImage = GetComponent<RawImage>();
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();
		}
		#endregion
	}
}
