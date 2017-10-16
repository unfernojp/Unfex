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
using Unfex.Events;

namespace Unfex.Commands {
	
	public class WaitEvent:Command {
		
		#region Fields
		object _target = null;
		string _name = "";
		#endregion
		
		
		
		#region Constructors
		public WaitEvent( object target, string name ) {
			_target = target;
			_name = name;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			UXEventDelegate.AddListener( _target, _name, _eventComplete );
			
			yield break;
		}
		
		protected override void cancelProcess() {
			UXEventDelegate.RemoveListener( _target, _name, _eventComplete );
		}
		#endregion
		
		
		
		#region EventHandlers
		void _eventComplete() {
			UXEventDelegate.RemoveListener( _target, _name, _eventComplete );
			
			done();
		}
		#endregion
	}
}
