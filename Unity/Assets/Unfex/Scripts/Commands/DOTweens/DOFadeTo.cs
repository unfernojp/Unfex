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
using UnityEngine.UI;
using Unfex.Impls;

namespace Unfex.Commands.DOTweens {

	public class DOFadeTo:DOTweenCommand {

		#region Fields
		CanvasGroup _target_canvasGroup = null;
		Image _target_image = null;
		Text _target_text = null;
		Material _target_material = null;
		SpriteRenderer _target_spriteRenderer = null;
		AudioSource _target_audioSource = null;
		IAlphable _target_alphable = null;

		float _endVal = 0.0f;
		#endregion



		#region Constructors
		public DOFadeTo( CanvasGroup target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_canvasGroup = target;
			_endVal = endVal;
		}

		public DOFadeTo( Image target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_image = target;
			_endVal = endVal;
		}

		public DOFadeTo( Text target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_text = target;
			_endVal = endVal;
		}

		public DOFadeTo( Material target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_material = target;
			_endVal = endVal;
		}

		public DOFadeTo( SpriteRenderer target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_spriteRenderer = target;
			_endVal = endVal;
		}

		public DOFadeTo( AudioSource target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_audioSource = target;
			_endVal = endVal;
		}

		public DOFadeTo( IAlphable target, float duration, float endVal, bool isRelative = false, Ease easeType = Ease.Linear, float delay = 0 ):base( duration, isRelative, easeType, delay ) {
			_target_alphable = target;
			_endVal = endVal;
		}
		#endregion



		#region Methods
		protected override Tweener build() {
			if ( _target_canvasGroup != null ) {
				if ( duration > 0 ) {
					return _target_canvasGroup.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_canvasGroup );

					_target_canvasGroup.alpha = _endVal;
				}
			} else if ( _target_image != null ) {
				if ( duration > 0 ) {
					return _target_image.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_image );

					_target_image.color = new Color( _target_image.color.r, _target_image.color.g, _target_image.color.b, _endVal );
				}
			} else if ( _target_text != null ) {
				if ( duration > 0 ) {
					return _target_text.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_text );

					_target_text.color = new Color( _target_text.color.r, _target_text.color.g, _target_text.color.b, _endVal );
				}
			}

			if ( _target_material != null ) {
				if ( duration > 0 ) {
					return _target_material.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_material );

					_target_material.color = new Color( _target_material.color.r, _target_material.color.g, _target_material.color.b, _endVal );
				}
			} else if ( _target_spriteRenderer != null ) {
				if ( duration > 0 ) {
					return _target_spriteRenderer.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_spriteRenderer );

					_target_spriteRenderer.color = new Color( _target_spriteRenderer.color.r, _target_spriteRenderer.color.g, _target_spriteRenderer.color.b, _endVal );
				}
			} else if ( _target_audioSource != null ) {
				if ( duration > 0 ) {
					return _target_audioSource.DOFade( _endVal, duration );
				} else {
					DOTween.Kill( _target_audioSource );

					_target_audioSource.volume = _endVal;
				}
			} else if ( _target_alphable != null ) {
				if ( duration > 0 ) {
					return DOTween.To( () => _target_alphable.alpha, x => _target_alphable.alpha = x, _endVal, duration );
				} else {
					DOTween.Kill( _target_alphable );

					_target_alphable.alpha = _endVal;
				}
			}

			return null;
		}
		#endregion
	}
}

