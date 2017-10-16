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

namespace Unfex.Events {

	public class DataEventData:UXEventData {

		#region Const Fields
		public const string onEndEdit = "onEndEdit";
		public const string onValidateInput = "onValidateInput";
		public const string onValueChanged = "onValueChanged";
		public const string onStateChanged = "onStateChanged";
		public const string onCountChanged = "onCountChanged";
		#endregion



		#region Properties
		public object data {
			get { return _data; }
		}
		object _data = null;
		#endregion



		#region Constructors
		public DataEventData( object target, string name, bool cancelable, object data = null ):base( target, name, cancelable ) {
			_data = data;
		}
		#endregion
	}
}
