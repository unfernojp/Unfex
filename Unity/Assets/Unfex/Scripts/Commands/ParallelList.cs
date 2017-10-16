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
	
	public enum ParallelListCompletionType { Anyone, Everyone }
	
	public class ParallelList:CommandList {
		
		#region Fields
		ParallelListCompletionType _completionType = ParallelListCompletionType.Everyone;
		#endregion
		
		
		
		#region Constructors
		public ParallelList() {
			_completionType = ParallelListCompletionType.Everyone;
			
			eventDelegate.RegisterEvent( UXEventData.onProgress );
		}
		
		public ParallelList( ParallelListCompletionType completionType ) {
			_completionType = completionType;
			
			eventDelegate.RegisterEvent( UXEventData.onProgress );
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			if ( total == 0 ) {
				done();
			} else {
				for ( int i = 0; i < _commandList.Count; i++ ) {
					Command command = _commandList[i] as Command;
					
					UXEventDelegate.AddListener( command, UXEventData.onComplete, _commandOnComplete );
				}
				
				for ( int i = 0; i < _commandList.Count; i++ ) {
					Command command = _commandList[i] as Command;
					
					command.Execute();
				}
			}
			
			yield break;
		}
		#endregion
		
		
		
		#region EventHandlers
		void _commandOnComplete() {
			UXEventData eventData = UXEventDelegate.GetEventData( _commandOnComplete );
			
			Command target = eventData.target as Command;
			
			if ( target == null ) { return; }
			
			UXEventDelegate.RemoveListener( target, UXEventData.onComplete, _commandOnComplete );
			
			UXEventDelegate.Execute( new UXEventData( this, UXEventData.onProgress, false ) );
			
			switch ( _completionType ) {
			case ParallelListCompletionType.Anyone :
				for ( int i = 0; i < _commandList.Count; i++ ) {
					Command command = _commandList[i] as Command;
					
					if ( command == target ) { continue; }
					
					UXEventDelegate.RemoveListener( command, UXEventData.onComplete, _commandOnComplete );
					
					command.Cancel();
					
					done();
				}
				break;
			case ParallelListCompletionType.Everyone :
				_count++;
				
				if ( _count >= _commandList.Count ) {
					done();
				}
				break;
			}
		}
		#endregion
	}
}
