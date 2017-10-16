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

using DG.Tweening;
using Unfex.Impls;

namespace Unfex.Commands.DOTweens {

	public class DOBlurTo:DOTweenCommand {

		#region Fields
		IBlurable _target_blurable = null;

		float _endVal;
		#endregion



		#region Constructors
		public DOBlurTo( IBlurable target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_blurable = target;
			_endVal = endVal;
		}
		#endregion



		#region Methods
		protected override Tweener build() {
			if ( _target_blurable != null ) {
				if ( duration > 0 ) {
					return DOTween.To( () => _target_blurable.blurSize, x => _target_blurable.blurSize = x, _endVal, duration );
				} else {
					DOTween.Kill( _target_blurable );

					_target_blurable.blurSize = _endVal;
				}
			}

			return null;
		}
		#endregion
	}
}

