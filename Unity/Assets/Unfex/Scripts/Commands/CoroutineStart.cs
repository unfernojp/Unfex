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
using UnityEngine;

namespace Unfex.Commands {
	
	public class CoroutineStart:Command {
		
		#region Fields
		IEnumerator _routine = null;

		object _target = null;
		string _methodName = null;
		MethodInfo _methodInfo = null;
		object[] _args = null;
		#endregion
		
		
		
		#region Constructors
		public CoroutineStart( object target, string methodName ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName );
		}

		public CoroutineStart( object target, string methodName, object[] args ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName );
			_args = args;
		}

		public CoroutineStart( object target, string methodName, BindingFlags bindingFlags ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName, bindingFlags );
		}

		public CoroutineStart( object target, string methodName, object[] args, BindingFlags bindingFlags ) {
			_target = target;
			_methodName = methodName;
			_methodInfo = _target.GetType().GetMethod( _methodName, bindingFlags );
			_args = args;
		}

		public CoroutineStart( object target, MethodInfo methodInfo ) {
			_target = target;
			_methodName = methodInfo.Name;
			_methodInfo = methodInfo;
		}

		public CoroutineStart( object target, MethodInfo methodInfo, object[] args ) {
			_target = target;
			_methodName = methodInfo.Name;
			_methodInfo = methodInfo;
			_args = args;
		}
		#endregion
		
		
		
		#region Methods
		protected override IEnumerator executeProcess() {
			if ( _methodInfo != null ) {
				_routine = _methodInfo.Invoke( _target, _args ) as IEnumerator;
			} else {
				Debug.LogWarning( string.Format( "Method {0} does not exist.", _methodName ) );
			}

			if ( _routine != null ) {
				yield return UXBehaviour.StartCoroutine( _routine );
			}
			
			done();

			_routine = null;
		}
		
		protected override void cancelProcess() {
			if ( _routine != null ) {
				UXBehaviour.StopCoroutine( _routine );

				_routine = null;
			}
		}
		#endregion
	}
}
