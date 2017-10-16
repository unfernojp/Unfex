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
using Unfex.Components;

namespace Unfex.Views.Core {

	public abstract class ProgressIndicatorBase:UXComponent {

		#region Methods
		public virtual IEnumerator Show() {
			yield break;
		}

		public virtual void Update( float current, float total ) {
			
		}

		public virtual IEnumerator Hide() {
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
		}

		protected override void OnDestroy() {
			base.OnDestroy();
		}
		#endregion
	}
}
