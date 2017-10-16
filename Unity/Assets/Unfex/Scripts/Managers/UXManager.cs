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

namespace Unfex.Managers {

	public sealed class UXManager:MonoBehaviour {

		#region Static Properties
		public static Dictionary<string, object> Parameters {
			get { return parameters; }
		}
		static Dictionary<string, object> parameters = new Dictionary<string, object>();
		#endregion



		#region Constructors
		private UXManager() {

		}
		#endregion
	}
}
