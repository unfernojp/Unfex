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

using UnityEngine;
using UnityEngine.EventSystems;

namespace Unfex.Events {

	public class InputEventData:DataEventData {

		#region Const Fields
		public const string onTouchDown = "onTouchDown";
		public const string onTouchUp = "onTouchUp";
		public const string onTap = "onTap";

		public const string onLongTap = "onLongTap";
		public const string onDoubleTap = "onDoubleTap";
		public const string on2FingerTap = "on2FingerTap";

		public const string onSwipeStart = "onSwipeStart";
		public const string onSwiping = "onSwiping";
		public const string onSwipeStop = "onSwipeStop";
		public const string onSwipeUp = "onSwipeUp";
		public const string onSwipeDown = "onSwipeDown";
		public const string onSwipeLeft = "onSwipeLeft";
		public const string onSwipeRight = "onSwipeRight";

		public const string onChargeStart = "onChargeStart";
		public const string onCharging = "onCharging";
		public const string onChargeStop = "onChargeEnd";

		public const string on2FingerChargeStart = "on2FingerChargeStart";
		public const string on2FingerCharging = "on2FingerCharging";
		public const string on2FingerChargeStop = "on2FingerChargeStop";

		public const string onClick = "onClick";

		public const string onMouse1Down = "onMouse1Down";
		public const string onMouse1Up = "onMouse1Up";
		public const string onMouse1Click = "onMouse1Click";

		public const string onMouse2Down = "onMouse2Down";
		public const string onMouse2Up = "onMouse2Up";
		public const string onMouse2Click = "onMouse2Click";

		public const string onMouseEnter = "onMouseEnter";
		public const string onMouseExit = "onMouseExit";

		public const string onMouseMoveStart = "onMouseMoveStart";
		public const string onMouseMove = "onMouseMove";
		public const string onMouseMoveStop = "onMouseMoveStop";

		public const string onMouseWheel = "onMouseWheel";

		public const string onKeyDown = "onKeyDown";
		public const string onKeyUp = "onKeyUp";
		#endregion



		#region Properties
		public PointerEventData pointerEventData {
			get { return _pointerEventData; }
		}
		PointerEventData _pointerEventData = null;

		public Vector2 inputPosition {
			get { return _inputPosition; }
		}
		Vector2 _inputPosition = Vector2.zero;

		public float wheelDelta {
			get { return _wheelDelta; }
		}
		float _wheelDelta = 0;
		#endregion



		#region Constructors
		public InputEventData( object target, string name, bool cancelable, object data = null, Vector2 inputPosition = default( Vector2 ), float wheelDelta = 0 ):base( target, name, cancelable, data ) {
			_pointerEventData = data as PointerEventData;
			_inputPosition = inputPosition;
			_wheelDelta = wheelDelta;
		}
		#endregion
	}
}
