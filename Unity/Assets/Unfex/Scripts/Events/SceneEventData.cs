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

	public class SceneEventData:UXEventData {

		#region Const Fields
		public const string onEnter = "onEnter";
		public const string onEnterOverlapUp = "onEnterOverlapUp";
		public const string onEnterOverlapDown = "onEnterOverlapDown";
		public const string onEnterOverlapSlide = "onEnterOverlapSlide";
		public const string onEnterPartUp = "onEnterPartUp";
		public const string onEnterPartDown = "onEnterPartDown";
		public const string onEnterPartSlide = "onEnterPartSlide";
		public const string onExit = "onExit";
		public const string onExitOverlapUp = "onExitOverlapUp";
		public const string onExitOverlapDown = "onExitOverlapDown";
		public const string onExitOverlapSlide = "onExitOverlapSlide";
		public const string onExitPartUp = "onExitPartUp";
		public const string onExitPartDown = "onExitPartDown";
		public const string onExitPartSlide = "onExitPartSlide";
		public const string onArrive = "onArrive";
		public const string onLeave = "onLeave";
		#endregion



		#region Properties
		public SceneEventData.Phase phase {
			get { return _phase; }
		}
		SceneEventData.Phase _phase = SceneEventData.Phase.Part;

		public SceneEventData.Direction direction {
			get { return _direction; }
		}
		SceneEventData.Direction _direction = SceneEventData.Direction.None;
		#endregion



		#region Fields
		public enum Phase { Part, Overlap }

		public enum Direction { None, Up, Down, Slide }
		#endregion



		#region Constructors
		public SceneEventData( object target, string name, bool cancelable, SceneEventData.Phase phase, SceneEventData.Direction direction ):base( target, name, cancelable ) {
			_phase = phase;
			_direction = direction;
		}
		#endregion
	}
}
