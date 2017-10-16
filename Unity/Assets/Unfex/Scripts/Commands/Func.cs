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
using System.Reflection;
using Unfex.Events;
using UnityEngine;

namespace Unfex.Commands {
	
	public class Func:Command {
		
		#region Fields
		public delegate void Callback();

		Callback _callback = null;

		object _target = null;
		string _methodName = "";
		MethodInfo _methodInfo = null;
		object[] _args = null;

		object _eventTarget = null;
		string _eventName = "";
		bool _hasDelegate = false;
		#endregion
		
		
		
		#region Constructors
		public Func( Callback callback ) {
			_callback = callback;
		}

		public Func( object target, string methodName ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName );
		}

		public Func( object target, string methodName, BindingFlags bindingFlags ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName, bindingFlags );
		}

		public Func( object target, string methodName, object[] args ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName );
			_args = args;
		}

		public Func( object target, string methodName, object[] args, BindingFlags bindingFlags ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName, bindingFlags );
			_args = args;
		}

		public Func( object target, MethodInfo methodInfo ) {
			_target = target;
			_methodName = methodInfo.Name;
			_methodInfo = methodInfo;
		}

		public Func( object target, MethodInfo methodInfo, object[] args ) {
			_target = target;
			_methodName = methodInfo.Name;
			_methodInfo = methodInfo;
			_args = args;
		}
		#endregion
		
		
		
		#region Methods
		public Func AddEvent( object target, string name ) {
			_eventTarget = target;
			_eventName = name;
			
			return this;
		}
		
		protected override IEnumerator executeProcess() {
			if ( _eventTarget != null && _eventName.Length > 0 ) {
				_hasDelegate = true;
				
				yield return null;
				
				UXEventDelegate.AddListener( _eventTarget, _eventName, _eventComplete );
			}

			if ( _callback != null ) {
				_callback.Invoke();
			} else if ( _methodInfo != null ) {
				_methodInfo.Invoke( _target, _args );
			} else {
				Debug.LogWarning( string.Format( "Method {0} does not exist.", _methodName ) );
			}

			if ( !_hasDelegate ) {
				done();
			}
		}
		
		protected override void cancelProcess() {
			
		}
		#endregion
		
		
		
		#region EventHandlers
		void _eventComplete() {
			UXEventDelegate.RemoveListener( _eventTarget, _eventName, _eventComplete );

			_eventTarget = null;
			_eventName = "";

			_hasDelegate = false;

			done();
		}
		#endregion
	}
}
