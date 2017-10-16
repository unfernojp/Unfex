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

	public class DOLocalMoveTo:DOTweenCommand {

		#region Fields
		Transform _target_transform = null;

		Vector3 _endVal;
		#endregion



		#region Constructors
		public DOLocalMoveTo( Transform target, float duration, Vector3 endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_transform = target;
			_endVal = endVal;
		}
		#endregion



		#region Methods
		protected override Tweener build() {
			if ( _target_transform != null ) {
				if ( duration > 0 ) {
					return _target_transform.DOLocalMove( _endVal, duration, true );
				} else {
					DOTween.Kill( _target_transform );

					_target_transform.localPosition = _endVal;
				}
			}

			return null;
		}
		#endregion
	}
}

