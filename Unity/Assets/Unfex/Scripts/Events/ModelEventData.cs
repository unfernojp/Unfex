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

namespace Unfex.Events {

	public class ModelEventData:UXEventData {

		#region Properties
		public string propName {
			get { return _propName; }
		}
		string _propName = "";

		public object newValue {
			get { return _newValue; }
		}
		object _newValue = null;

		public object oldValue {
			get { return _oldValue; }
		}
		object _oldValue = null;
		#endregion



		#region Constructors
		public ModelEventData( object target, string name, bool cancelable, string propName, object newValue, object oldValue ):base( target, name, cancelable ) {
			_propName = propName;
			_newValue = newValue;
			_oldValue = oldValue;
		}
		#endregion
	}
}
