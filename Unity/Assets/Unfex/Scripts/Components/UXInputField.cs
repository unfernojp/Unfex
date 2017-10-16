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
using UnityEngine.UI;
using Unfex.Events;

namespace Unfex.Components {

	[RequireComponent( typeof( InputField ) )]
	public class UXInputField:UXComponent {

		#region Properties
		public InputField inputField {
			get { return _inputField; }
		}
		InputField _inputField = null;

		public bool interactable {
			get { return _inputField.interactable; }
			set { _inputField.interactable = value; }
		}

		public char asteriskChar {
			get { return _inputField.asteriskChar; }
			set { _inputField.asteriskChar = value; }
		}

		public float caretBlinkRate {
			get { return _inputField.caretBlinkRate; }
			set { _inputField.caretBlinkRate = value; }
		}

		public Color caretColor {
			get { return _inputField.caretColor; }
			set { _inputField.caretColor = value; }
		}

		public int caretPosition {
			get { return _inputField.caretPosition; }
			set { _inputField.caretPosition = value; }
		}

		public int caretWidth {
			get { return _inputField.caretWidth; }
			set { _inputField.caretWidth = value; }
		}

		public int characterLimit {
			get { return _inputField.characterLimit; }
			set { _inputField.characterLimit = value; }
		}

		public InputField.CharacterValidation characterValidation {
			get { return _inputField.characterValidation; }
			set { _inputField.characterValidation = value; }
		}

		public InputField.ContentType contentType {
			get { return _inputField.contentType; }
			set { _inputField.contentType = value; }
		}

		public bool customCaretColor {
			get { return _inputField.customCaretColor; }
			set { _inputField.customCaretColor = value; }
		}

		public InputField.InputType inputType {
			get { return _inputField.inputType; }
			set { _inputField.inputType = value; }
		}

		public bool isFocused {
			get { return _inputField.isFocused; }
		}

		public TouchScreenKeyboardType keyboardType {
			get { return _inputField.keyboardType; }
			set { _inputField.keyboardType = value; }
		}

		public InputField.LineType lineType {
			get { return _inputField.lineType; }
			set { _inputField.lineType = value; }
		}

		public bool multiLine {
			get { return _inputField.multiLine; }
		}

		public Graphic placeholder {
			get { return _inputField.placeholder; }
			set { _inputField.placeholder = value; }
		}

		public bool readOnly {
			get { return _inputField.readOnly; }
			set { _inputField.readOnly = value; }
		}

		public int selectionAnchorPosition {
			get { return _inputField.selectionAnchorPosition; }
			set { _inputField.selectionAnchorPosition = value; }
		}

		public Color selectionColor {
			get { return _inputField.selectionColor; }
			set { _inputField.selectionColor = value; }
		}

		public int selectionFocusPosition {
			get { return _inputField.selectionFocusPosition; }
			set { _inputField.selectionFocusPosition = value; }
		}

		public bool shouldHideMobileInput {
			get { return _inputField.shouldHideMobileInput; }
			set { _inputField.shouldHideMobileInput = value; }
		}

		public string text {
			get { return _inputField.text; }
			set { _inputField.text = value; }
		}

		public Text textComponent {
			get { return _inputField.textComponent; }
			set { _inputField.textComponent = value; }
		}

		public bool wasCanceled {
			get { return _inputField.wasCanceled; }
		}
		#endregion



		#region Methods
		public void ActivateInputField() {
			_inputField.ActivateInputField();
		}

		public void DeactivateInputField() {
			_inputField.DeactivateInputField();
		}

		public void ForceLabelUpdate() {
			_inputField.ForceLabelUpdate();
		}

		public void GraphicUpdateComplete() {
			_inputField.GraphicUpdateComplete();
		}

		public void LayoutComplete() {
			_inputField.LayoutComplete();
		}

		public void MoveTextEnd( bool shift ) {
			_inputField.MoveTextEnd( shift );
		}

		public void MoveTextStart( bool shift ) {
			_inputField.MoveTextStart( shift );
		}

		public void ProcessEvent( Event e ) {
			_inputField.ProcessEvent( e );
		}

		public void Rebuild( CanvasUpdate update ) {
			_inputField.Rebuild( update );
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onEndEdit );
			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_inputField = GetComponent<InputField>();
			_inputField.onEndEdit.AddListener( _onEndEdit );
			_inputField.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_inputField.onEndEdit.RemoveListener( _onEndEdit );
			_inputField.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onEndEdit( string value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onEndEdit, false, value ) );
		}

		void _onValueChanged( string value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
