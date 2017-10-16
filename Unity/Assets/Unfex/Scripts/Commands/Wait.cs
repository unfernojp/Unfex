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

using System.Collections;

namespace Unfex.Commands {
	
	public class Wait:Command {
		
		#region Constructors
		public Wait( float time ) {
			base.Delay( time );
		}
		#endregion
		
		
		
		#region Methods
		public new Command Delay( float delay = 0 ) {
			return this;
		}

		protected override IEnumerator executeProcess() {
			done();
			
			yield break;
		}
		
		protected override void cancelProcess() {
			
		}
		#endregion
	}
}
