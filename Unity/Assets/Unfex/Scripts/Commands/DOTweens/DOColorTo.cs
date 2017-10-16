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
using UnityEngine;
using UnityEngine.UI;

namespace Unfex.Commands.DOTweens {

	public class DOColorTo:DOTweenCommand {

		#region Fields
		Image _target_image = null;
		Material _target_material = null;
		SpriteRenderer _target_spriteRenderer = null;

		Color _endVal = Color.black;
		#endregion



		#region Constructors
		public DOColorTo( Image target, float duration, Color endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_image = target;
			_endVal = endVal;
		}

		public DOColorTo( Material target, float duration, Color endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_material = target;
			_endVal = endVal;
		}

		public DOColorTo( SpriteRenderer target, float duration, Color endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_spriteRenderer = target;
			_endVal = endVal;
		}
		#endregion



		#region Methods
		protected override Tweener build() {
			if ( _target_image != null ) {
				if ( duration > 0 ) {
					return _target_image.DOColor( _endVal, duration );
				} else {
					DOTween.Kill( _target_image );

					_target_image.color = _endVal;
				}
			} else if ( _target_material != null ) {
				if ( duration > 0 ) {
					return _target_material.DOColor( _endVal, duration );
				} else {
					DOTween.Kill( _target_material );

					_target_material.color = _endVal;
				}
			} else if ( _target_spriteRenderer != null ) {
				if ( duration > 0 ) {
					return _target_spriteRenderer.DOColor( _endVal, duration );
				} else {
					DOTween.Kill( _target_spriteRenderer );

					_target_spriteRenderer.color = _endVal;
				}
			}

			return null;
		}
		#endregion
	}
}

