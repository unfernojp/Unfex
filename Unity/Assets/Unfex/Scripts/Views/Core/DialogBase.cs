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
using Unfex.Events;

namespace Unfex.Views.Core {

	public abstract class DialogBase:PageBase {

		#region Properties
		public CanvasGroup container {
			get { return _container; }
		}
		[SerializeField] CanvasGroup _container = null;

		public CanvasGroup background {
			get { return _background; }
		}
		[SerializeField] CanvasGroup _background = null;
		#endregion



		#region Methods
		protected void OK() {
			OK( new object[]{} );
		}

		protected void OK( object[] args ) {
			UXEventDelegate.Execute( new DataEventData( this, UXEventData.onOK, false, args ) );
		}

		protected void Cancel() {
			UXEventDelegate.Execute( new DataEventData( this, UXEventData.onCancel, false ) );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( UXEventData.onOK );
			eventDelegate.RegisterEvent( UXEventData.onCancel );
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
