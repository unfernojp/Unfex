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

using UnityEngine.EventSystems;

namespace Unfex.Events {

	public class UXEventData:BaseEventData {

		#region Const Fields
		public const string onInit = "onInit";
		public const string onReady = "onReady";
		public const string onStart = "onStart";
		public const string onStop = "onStop";
		public const string onOpen = "onOpen";
		public const string onOpened = "onOpened";
		public const string onClose = "onClose";
		public const string onClosed = "onClosed";
		public const string onProgress = "onProgress";
		public const string onComplete = "onComplete";
		public const string onSelect = "onSelect";
		public const string onUnselect = "onUnselect";
		public const string onChange = "onChange";
		public const string onRefresh = "onRefresh";
		public const string onUpdate = "onUpdate";
		public const string onLateUpdate = "onLateUpdate";
		public const string onFocus = "onFocus";
		public const string onBlur = "onBlur";
		public const string onOK = "onOK";
		public const string onCancel = "onCancel";
		public const string onSubmit = "onSubmit";
		public const string onInvoke = "onInvoke";
		public const string onError = "onError";
		#endregion



		#region Properties
		public object target {
			get { return _target; }
		}
		object _target = null;

		public string name {
			get { return _name; }
		}
		string _name = "";

		public bool cancelable {
			get { return _cancelable; }
		}
		bool _cancelable = false;
		#endregion



		#region Fields
		bool _preventDefault = false;
		#endregion



		#region Constructors
		public UXEventData( object target, string name, bool cancelable = false ):base( target as EventSystem ) {
			_target = target;
			_name = name;
			_cancelable = cancelable;
		}
		#endregion



		#region Methods
		public bool IsDefaultPrevented() {
			return _preventDefault;
		}

		public void PreventDefault() {
			if ( _cancelable ) {
				_preventDefault = true;
			}
		}
		#endregion
	}
}
