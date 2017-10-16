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

using DG.Tweening;
using UnityEngine;

namespace Unfex.Commands.DOTweens {

	public class DOAnchorPosTo:DOTweenCommand {

		#region Fields
		RectTransform _target_rectTransform = null;

		Vector2 _endVal;
		#endregion



		#region Constructors
		public DOAnchorPosTo( RectTransform target, float duration, Vector2 endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_rectTransform = target;
			_endVal = endVal;
		}
		#endregion



		#region Methods
		protected override Tweener build() {
			if ( _target_rectTransform != null ) {
				if ( duration > 0 ) {
					return _target_rectTransform.DOAnchorPos( _endVal, duration );
				} else {
					DOTween.Kill( _target_rectTransform );

					_target_rectTransform.anchoredPosition = _endVal;
				}
			}

			return null;
		}
		#endregion
	}
}

