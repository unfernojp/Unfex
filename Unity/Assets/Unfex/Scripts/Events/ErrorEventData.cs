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

namespace Unfex.Events {

	public class ErrorEventData:UXEventData {

		#region Properties
		public Exception errorObject {
			get { return _errorObject; }
		}
		Exception _errorObject = null;
		#endregion



		#region Constructors
		public ErrorEventData( object target, string name, bool cancelable, Exception errorObject ):base( target, name, cancelable ) {
			_errorObject = errorObject;
		}
		#endregion
	}
}
