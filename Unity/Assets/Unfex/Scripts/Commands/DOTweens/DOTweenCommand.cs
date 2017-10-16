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
using DG.Tweening;
using Unfex.Commands;

namespace Unfex.Commands.DOTweens {

	public class DOTweenCommand:Command {

		#region Properties
		public float duration {
			get { return _duration; }
		}
		float _duration = 0.0f;

		public bool isRelative {
			get { return _isRelative; }
		}
		bool _isRelative = false;

		public Ease easeType {
			get { return _easeType; }
		}
		Ease _easeType = Ease.Linear;

		public float delay {
			get { return _delay; }
		}
		float _delay = 0.0f;
		#endregion



		#region Fields
		Tweener _tweener = null;
		#endregion



		#region Constructors
		public DOTweenCommand( float duration, bool isRelative, Ease easeType, float delay ) {
			_duration = duration;
			_isRelative = isRelative;
			_easeType = easeType;
			_delay = delay;
		}
		#endregion



		#region Methods
		protected virtual Tweener build() {
			return null;
		}

		protected override IEnumerator executeProcess() {
			_tweener = build();

			if ( _tweener != null ) {
				_tweener.SetRelative( _isRelative ).SetEase( _easeType ).SetDelay( _delay ).OnComplete( _tweenerOnCompleteAndKill ).OnKill( _tweenerOnCompleteAndKill );
			} else {
				done();
			}

			yield break;
		}

		protected override void cancelProcess() {
			if ( _tweener != null ) {
				if ( _tweener.IsActive() ) {
					_tweener.Kill();
				}

				_tweener = null;
			}
		}
		#endregion



		#region EventHandlers
		void _tweenerOnCompleteAndKill() {
			if ( _tweener != null ) {
				if ( _tweener.IsActive() ) {
					_tweener.Kill();
				}

				_tweener = null;
			}

			done();
		}
		#endregion
	}
}

