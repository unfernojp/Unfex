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

using System.Collections.Generic;
using UnityEngine;

namespace Unfex.Models {

	[System.Serializable]
	public abstract class Model {

		#region Fields
		[SerializeField] protected Dictionary<string, object> properties = new Dictionary<string, object>();
		#endregion



		#region Constructors
		public Model() {

		}
		#endregion



		#region Methods
		protected T getProperty<T>( string name ) {
			if ( properties.ContainsKey( name ) ) { return (T)(object)properties[name]; }

			return default( T );
		}

		protected T setProperty<T>( string name, T value ) {
			properties[name] = value;

			return value;
		}
		#endregion
	}
}
