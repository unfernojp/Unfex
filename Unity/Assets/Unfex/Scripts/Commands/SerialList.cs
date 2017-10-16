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

using System.Collections;
using Unfex.Events;

namespace Unfex.Commands {
	
	public class SerialList:CommandList {
		
		#region Fields
		Command _command = null;
		#endregion
		
		
		
		#region Constructors
		public SerialList() {
			eventDelegate.RegisterEvent( UXEventData.onProgress );
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			if ( total == 0 ) {
				done();
			} else {
				_count = 0;
				
				_process();
			}
			
			yield break;
		}
		
		void _process() {
			if ( count < total ) {
				_command = _commandList[count];
				
				UXEventDelegate.AddListener( _command, UXEventData.onComplete, _commandOnComplete );
				
				_command.Execute();
			}
			else {
				done();
			}
		}
		#endregion
		
		
		
		#region EventHandlers
		void _commandOnComplete() {
			UXEventDelegate.RemoveListener( _command, UXEventData.onComplete, _commandOnComplete );
			
			UXEventDelegate.Execute( new UXEventData( this, UXEventData.onProgress, false ) );
			
			_count++;
			
			_process();
		}
		#endregion
	}
}
