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

	public class ViewEventData:UXEventData {

		#region Const Fields
		public const string onShow= "onShow";
		public const string onHide= "onHide";
		#endregion



		#region Constructors
		public ViewEventData( object target, string name, bool cancelable ):base( target, name, cancelable ) {
			
		}
		#endregion
	}
}
