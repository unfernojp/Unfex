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
using Unfex.Events;
using UnityEngine;

namespace Unfex.Commands {
	
	public abstract class Command:IEventDelegatable {
		
		#region Properties
		public UXEventDelegate eventDelegate {
			get { return _eventDelegate; }
		}
		UXEventDelegate _eventDelegate = null;

		public Command.State state {
			get { return _state; }
		}
		Command.State _state = Command.State.Idling;
		#endregion
		
		
		
		#region Fields
		public enum State { Idling, Executing, Canceling, Erroring }

		float _delay = 0.0f;
		
		IEnumerator _executeCoroutine = null;
		IEnumerator _processCoroutine = null;
		#endregion
		
		
		
		#region Constructors
		public Command() {
			_eventDelegate = new UXEventDelegate( this );
			_eventDelegate.RegisterEvent( UXEventData.onStart );
			_eventDelegate.RegisterEvent( UXEventData.onComplete );
			_eventDelegate.RegisterEvent( UXEventData.onCancel );
		}
		#endregion
		
		
		
		#region Methods
		public Command Delay( float delay = 0 ) {
			_delay = delay;
			
			return this;
		}
		
		public Coroutine Execute() {
			if ( _state != Command.State.Idling ) { throw new Exception( string.Format( "Command {0} is already running.", this ) ); }
			
			_state = Command.State.Executing;
			
			if ( UXEventDelegate.Execute( new UXEventData( this, UXEventData.onStart, true ) ) ) {
				_executeCoroutine = _execute();
				
				return UXBehaviour.StartCoroutine( _executeCoroutine );
			} else {
				return null;
			}
		}
		
		IEnumerator _execute() {
			if ( _delay > 0 ) {
				yield return new WaitForSeconds( _delay );
			}
			
			_processCoroutine = executeProcess();
			
			UXBehaviour.StartCoroutine( _processCoroutine );
			
			while (_state == Command.State.Executing ) {
				yield return null;
			}

			_executeCoroutine = null;
			_processCoroutine = null;
		}
		
		protected abstract IEnumerator executeProcess();
		
		public void Cancel() {
			if ( _state != Command.State.Executing ) { return; }
			
			_state = Command.State.Canceling;

			if ( _executeCoroutine != null ) {
				UXBehaviour.StopCoroutine( _executeCoroutine );

				if ( _processCoroutine != null ) {
					UXBehaviour.StopCoroutine( _processCoroutine );
				}
			}

			_executeCoroutine = null;
			_processCoroutine = null;

			cancelProcess();
			
			_state = Command.State.Idling;
			
			UXEventDelegate.Execute( new UXEventData( this, UXEventData.onCancel, false ) );
		}
		
		protected abstract void cancelProcess();
		
		protected void done() {
			if ( _state != Command.State.Executing ) { return; }
			
			_state = Command.State.Idling;
			
			UXEventDelegate.Execute( new UXEventData( this, UXEventData.onComplete, false ) );
		}
		#endregion
	}
}
