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

	[RequireComponent( typeof( Image ) )]
	public class UXImage:UXComponent {

		#region Properties
		public Image image {
			get { return _image; }
		}
		Image _image = null;

		public float alpha {
			get { return _image.color.a; }
			set { _image.color = new Color( _image.color.r, _image.color.g, _image.color.b, value ); }
		}

		public float alphaHitTestMinimumThreshold {
			get { return _image.alphaHitTestMinimumThreshold; }
			set { _image.alphaHitTestMinimumThreshold = value; }
		}

		public float fillAmount {
			get { return _image.fillAmount; }
			set { _image.fillAmount = value; }
		}

		public bool fillCenter {
			get { return _image.fillCenter; }
			set { _image.fillCenter = value; }
		}

		public bool fillClockwise {
			get { return _image.fillClockwise; }
			set { _image.fillClockwise = value; }
		}

		public Image.FillMethod fillMethod {
			get { return _image.fillMethod; }
			set { _image.fillMethod = value; }
		}

		public int fillOrigin {
			get { return _image.fillOrigin; }
			set { _image.fillOrigin = value; }
		}

		public float flexibleHeight {
			get { return _image.flexibleHeight; }
		}

		public float flexibleWidth {
			get { return _image.flexibleWidth; }
		}

		public bool hasBorder {
			get { return _image.hasBorder; }
		}

		public int layoutPriority {
			get { return _image.layoutPriority; }
		}

		public Texture mainTexture {
			get { return _image.mainTexture; }
		}

		public Material material {
			get { return _image.material; }
			set { _image.material = value; }
		}

		public float minHeight {
			get { return _image.minHeight; }
		}

		public float minWidth {
			get { return _image.minWidth; }
		}

		public Sprite overrideSprite {
			get { return _image.overrideSprite; }
			set { _image.overrideSprite = value; }
		}

		public float preferredHeight {
			get { return _image.preferredHeight; }
		}

		public float preferredWidth {
			get { return _image.preferredWidth; }
		}

		public bool preserveAspect {
			get { return _image.preserveAspect; }
			set { _image.preserveAspect = value; }
		}

		public Sprite sprite {
			get { return _image.sprite; }
			set { _image.sprite = value; }
		}

		public Image.Type type {
			get { return _image.type; }
			set { _image.type = value; }
		}
		#endregion



		#region Methods
		public void CalculateLayoutInputHorizontal() {
			_image.CalculateLayoutInputHorizontal();
		}

		public void CalculateLayoutInputVertical() {
			_image.CalculateLayoutInputVertical();
		}

		public bool CalculateLayoutInputVertical( Vector2 screenPoint, Camera eventCamera ) {
			return _image.IsRaycastLocationValid( screenPoint, eventCamera );
		}

		public void SetNativeSize() {
			_image.SetNativeSize();
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			_image = GetComponent<Image>();
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
				_image = null;
			}
		}
		#endregion
	}
}
