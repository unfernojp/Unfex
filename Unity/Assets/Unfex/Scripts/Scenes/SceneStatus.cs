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

using Unfex.Events;

namespace Unfex.Scenes {

	public class SceneStatus {

		#region Properties
		public string eventType {
			get { return _eventType; }
		}
		string _eventType = "";

		public SceneEventData.Phase phase {
			get { return _phase; }
		}
		SceneEventData.Phase _phase = SceneEventData.Phase.Part;

		public SceneEventData.Direction direction {
			get { return _direction; }
		}
		SceneEventData.Direction _direction = SceneEventData.Direction.None;

		public bool slidePolicy {
			get { return _slidePolicy; }
		}
		bool _slidePolicy = false;
		#endregion



		#region Constructors
		public SceneStatus( string eventType, SceneEventData.Phase phase, SceneEventData.Direction direction, bool slidePolicy ) {
			_eventType = eventType;
			_phase = phase;
			_direction = direction;
			_slidePolicy = slidePolicy;
		}
		#endregion
	}
}
