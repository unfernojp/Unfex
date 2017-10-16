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
	
	public class DebugLog:Command {
		
		#region Fields
		object[] _expressions = null;
		#endregion
		
		
		
		#region Constructors
		public DebugLog( params object[] expressions ) {
			_expressions = expressions;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			string[] messages = new string[_expressions.Length];
			
			for ( int i = 0; i < _expressions.Length; i++ ) {
				messages[i] = _expressions[i].ToString();
			}
			
			Debug.Log( string.Join( ", ", messages ) );
			
			done();
			
			yield break;
		}
		
		protected override void cancelProcess() {
			
		}
		#endregion
	}
}
