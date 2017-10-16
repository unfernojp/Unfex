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

using System;
using UnityEngine.UI;

namespace Unfex.Utils {

	public sealed class TextUtil {

		#region Static Methods
		public static void Abbreviate( Text text, string ellipsis = "..." ) {
			if ( text == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetGlobalPosition()", "text" ) ); }

			float textWidth = text.preferredWidth;
			float maxWidth = text.rectTransform.sizeDelta.x;

			string value = text.text;

			while ( textWidth > maxWidth ) {
				value = value.Substring( 0, Math.Max( 0, value.Length - 1 ) );

				if ( value.Length == 0 ) { break; }

				text.text = value + ellipsis;
				textWidth = text.preferredWidth;
			}
		}
		#endregion
	}
}
