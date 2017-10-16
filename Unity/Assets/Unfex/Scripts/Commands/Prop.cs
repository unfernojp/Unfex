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

using System;
using System.Collections;
using System.Reflection;

namespace Unfex.Commands {
	
	public class Prop:Command {
		
		#region Fields
		object _target = null;
		string _propName = "";
		object _propValue = null;
		#endregion
		
		
		
		#region Constructors
		public Prop( object target, string propName, object propValue ) {
			_target = target;
			_propName = propName;
			_propValue = propValue;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			Type type = _target.GetType();
			PropertyInfo info = type.GetProperty( _propName );
			
			if ( info != null ) {
				info.SetValue( _target, _propValue, null );
			}
			
			done();
			
			yield break;
		}
		
		protected override void cancelProcess() {
			
		}
		#endregion
	}
}
