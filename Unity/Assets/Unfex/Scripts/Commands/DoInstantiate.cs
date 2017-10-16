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
using UnityEngine;

namespace Unfex.Commands {
	
	public class DoInstantiate:Command {
		
		#region Fields
		GameObject _target = null;
		GameObject _parent = null;
		Vector3 _position = Vector3.zero;
		#endregion
		
		
		
		#region Constructors
		public DoInstantiate( GameObject target, MonoBehaviour parent, Vector3 position ) {
			_target = target;
			_parent = parent.gameObject;
			_position = position;
		}
		
		public DoInstantiate( GameObject target, GameObject parent, Vector3 position ) {
			_target = target;
			_parent = parent;
			_position = position;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			try {
				GameObject gameObject = MonoBehaviour.Instantiate( _target ) as GameObject;
				gameObject.transform.parent = _parent.transform;
				gameObject.transform.localPosition = _position;
			} finally {
				done();
			}
			
			yield break;
		}
		
		protected override void cancelProcess() {
			
		}
		#endregion
	}
}
