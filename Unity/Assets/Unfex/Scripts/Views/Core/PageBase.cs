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
using UnityEngine.UI;

namespace Unfex.Views.Core {

	public abstract class PageBase:UXComponent {

		#region Methods
		public virtual IEnumerator Show() {
			yield break;
		}

		public virtual IEnumerator Hide() {
			yield break;
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( UXEventData.onFocus );
			eventDelegate.RegisterEvent( UXEventData.onBlur );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		public virtual void OnFocus() {
			
		}

		public virtual void OnBlur() {
			
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			Image[] images = GetComponentsInChildren<Image>( true );

			for ( int i = 0; i < images.Length; i++ ) {
				Image image = images[i];

				image.sprite = null;
			}

			Button[] buttons = GetComponentsInChildren<Button>( true );

			for ( int i = 0; i < buttons.Length; i++ ) {
				Button button = buttons[i];

				button.spriteState = default( SpriteState );
			}
		}
		#endregion
	}
}
