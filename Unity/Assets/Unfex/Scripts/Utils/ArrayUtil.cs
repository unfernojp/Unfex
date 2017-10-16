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
using System.Collections.Generic;
using System.Reflection;

namespace Unfex.Utils {

	public sealed class ArrayUtil {

		#region Static Methods
		public static T[] SortOn<T>( string fieldName, T[] items ) {
			List<object> keyList = new List<object>();

			for ( int i = 0; i < items.Length; i++ ) {
				T item = items[i];

				PropertyInfo propertyInfo = item.GetType().GetProperty( fieldName );

				if ( propertyInfo != null ) {
					keyList.Add( propertyInfo.GetValue( item, null ) );
					continue;
				}

				FieldInfo fieldInfo = item.GetType().GetField( fieldName );

				if ( fieldInfo != null ) {
					keyList.Add( fieldInfo.GetValue( item ) );
					continue;
				}
			}

			object[] keys = keyList.ToArray();
			items = items.Clone() as T[];

			Array.Sort( keys, items );

			return items;
		}
		#endregion
	}
}
