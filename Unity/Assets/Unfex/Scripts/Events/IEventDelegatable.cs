﻿/**
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

	public interface IEventDelegatable {

		#region Properties
		UXEventDelegate eventDelegate {
			get;
		}
		#endregion
	}
}
