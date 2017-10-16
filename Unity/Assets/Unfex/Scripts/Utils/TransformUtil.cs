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
using UnityEngine;

namespace Unfex.Utils {
	
	public sealed class TransformUtil {
		
		#region Static Methods
		public static Vector3 GetGlobalPosition( Transform transform ) {
			if ( transform == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetGlobalPosition()", "transform" ) ); }
			
			Vector3 position = Vector3.zero;
			
			Transform current = transform;
			
			while ( current != null ) {
				position.x += current.localPosition.x;
				position.y += current.localPosition.y;
				position.z += current.localPosition.z;
				
				current = current.parent;
			}
			
			return position;
		}
		#endregion
	}
}
