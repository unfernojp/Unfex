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

namespace Unfex.Components {

	public class UXComponent:UXBehaviour, IEventDelegatable {

		#region Properties
		public UXEventDelegate eventDelegate {
			get { return _eventDelegate; }
		}
		UXEventDelegate _eventDelegate = null;

		public CanvasGroup canvasGroup {
			get { return _canvasGroup; }
		}
		CanvasGroup _canvasGroup = null;
		#endregion



		#region Methods
		public virtual void Kill() {
			UXBehaviour.Destroy( gameObject );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			_eventDelegate = new UXEventDelegate( this );

			_canvasGroup = GetComponent<CanvasGroup>();
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_eventDelegate = null;
		}
		#endregion
	}
}
