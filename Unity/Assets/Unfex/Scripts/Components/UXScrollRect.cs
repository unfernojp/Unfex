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

	[RequireComponent( typeof( ScrollRect ) )]
	public class UXScrollRect:UXComponent {

		#region Properties
		public ScrollRect scrollRect {
			get { return _scrollRect; }
		}
		ScrollRect _scrollRect = null;

		public RectTransform content {
			get { return _scrollRect.content; }
			set { _scrollRect.content = value; }
		}

		public float decelerationRate {
			get { return _scrollRect.decelerationRate; }
			set { _scrollRect.decelerationRate = value; }
		}

		public float elasticity {
			get { return _scrollRect.elasticity; }
			set { _scrollRect.elasticity = value; }
		}

		public float flexibleHeight {
			get { return _scrollRect.flexibleHeight; }
		}

		public float flexibleWidth {
			get { return _scrollRect.flexibleWidth; }
		}

		public bool horizontal {
			get { return _scrollRect.horizontal; }
			set { _scrollRect.horizontal = value; }
		}

		public float horizontalNormalizedPosition {
			get { return _scrollRect.horizontalNormalizedPosition; }
			set { _scrollRect.horizontalNormalizedPosition = value; }
		}

		public Scrollbar horizontalScrollbar {
			get { return _scrollRect.horizontalScrollbar; }
			set { _scrollRect.horizontalScrollbar = value; }
		}

		public float horizontalScrollbarSpacing {
			get { return _scrollRect.horizontalScrollbarSpacing; }
			set { _scrollRect.horizontalScrollbarSpacing = value; }
		}

		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility {
			get { return _scrollRect.horizontalScrollbarVisibility; }
			set { _scrollRect.horizontalScrollbarVisibility = value; }
		}

		public bool inertia {
			get { return _scrollRect.inertia; }
			set { _scrollRect.inertia = value; }
		}

		public int layoutPriority {
			get { return _scrollRect.layoutPriority; }
		}

		public float minHeight {
			get { return _scrollRect.minHeight; }
		}

		public float minWidth {
			get { return _scrollRect.minWidth; }
		}

		public ScrollRect.MovementType movementType {
			get { return _scrollRect.movementType; }
			set { _scrollRect.movementType = value; }
		}

		public Vector2 normalizedPosition {
			get { return _scrollRect.normalizedPosition; }
			set { _scrollRect.normalizedPosition = value; }
		}

		public float preferredHeight {
			get { return _scrollRect.preferredHeight; }
		}

		public float preferredWidth {
			get { return _scrollRect.preferredWidth; }
		}

		public float scrollSensitivity {
			get { return _scrollRect.scrollSensitivity; }
			set { _scrollRect.scrollSensitivity = value; }
		}

		public Vector2 velocity {
			get { return _scrollRect.velocity; }
			set { _scrollRect.velocity = value; }
		}

		public bool vertical {
			get { return _scrollRect.vertical; }
			set { _scrollRect.vertical = value; }
		}

		public float verticalNormalizedPosition {
			get { return _scrollRect.verticalNormalizedPosition; }
			set { _scrollRect.verticalNormalizedPosition = value; }
		}

		public Scrollbar verticalScrollbar {
			get { return _scrollRect.verticalScrollbar; }
			set { _scrollRect.verticalScrollbar = value; }
		}

		public float verticalScrollbarSpacing {
			get { return _scrollRect.verticalScrollbarSpacing; }
			set { _scrollRect.verticalScrollbarSpacing = value; }
		}

		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility {
			get { return _scrollRect.verticalScrollbarVisibility; }
			set { _scrollRect.verticalScrollbarVisibility = value; }
		}

		public RectTransform viewport {
			get { return _scrollRect.viewport; }
			set { _scrollRect.viewport = value; }
		}
		#endregion



		#region Methods
		public void CalculateLayoutInputHorizontal() {
			_scrollRect.CalculateLayoutInputHorizontal();
		}

		public void CalculateLayoutInputVertical() {
			_scrollRect.CalculateLayoutInputVertical();
		}

		public void GraphicUpdateComplete() {
			_scrollRect.GraphicUpdateComplete();
		}

		public bool IsActive() {
			return _scrollRect.IsActive();
		}

		public void LayoutComplete() {
			_scrollRect.LayoutComplete();
		}

		public void Rebuild( CanvasUpdate update ) {
			_scrollRect.Rebuild( update );
		}

		public void SetLayoutHorizontal() {
			_scrollRect.SetLayoutHorizontal();
		}

		public void SetLayoutVertical() {
			_scrollRect.SetLayoutVertical();
		}

		public void StopMovement() {
			_scrollRect.StopMovement();
		}
		#endregion



		#region EventHandlers
		protected override void Awake() {
			base.Awake();

			eventDelegate.RegisterEvent( DataEventData.onValueChanged );

			_scrollRect = GetComponent<ScrollRect>();
			_scrollRect.onValueChanged.AddListener( _onValueChanged );
		}

		protected override void Init() {
			base.Init();
		}

		protected override void Start() {
			base.Start();
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_scrollRect.onValueChanged.RemoveListener( _onValueChanged );
		}

		void _onValueChanged( Vector2 value ) {
			UXEventDelegate.Execute( new DataEventData( this, DataEventData.onValueChanged, false, value ) );
		}
		#endregion
	}
}
