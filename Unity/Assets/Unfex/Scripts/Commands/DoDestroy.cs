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
	
	public class DoDestroy:Command {
		
		#region Fields
		GameObject _target = null;
		#endregion
		
		
		
		#region Constructors
		public DoDestroy( MonoBehaviour target ) {
			_target = target.gameObject;
		}
		
		public DoDestroy( GameObject target ) {
			_target = target;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			try {
				MonoBehaviour.Destroy( _target );
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
