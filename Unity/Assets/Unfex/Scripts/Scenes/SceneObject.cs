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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unfex.Managers;
using UnityEngine;

namespace Unfex.Scenes {

	public class SceneObject {

		#region Indexers
		public SceneObject this[string name] {
			get { return GetChildByName( name ); }
		}

		public SceneObject this[int index] {
			get { return GetChildAt( index ); }
		}
		#endregion



		#region Properties
		public virtual string name {
			get { return _name; }
			set {
				if ( !new Regex( "^[_a-z0-9]+$", RegexOptions.IgnoreCase ).IsMatch( value ) ) { throw new Exception( "The property name contains prohibited characters: " + value ); }

				_name = value;
			}
		}
		string _name = "";

		public virtual SceneId sceneId {
			get {
				if ( root == null ) { return SceneId.NaS; }
				if ( root == this ) { return new SceneId( "/" ); }

				string path = "";
				SceneObject scene = this;

				while ( scene != null ) {
					if ( root == scene ) { break; }

					path = scene.name + "/" + path;
					scene = scene.parent;
				}

				return new SceneId( "/" + path );
			}
		}

		public virtual SceneObject root {
			get {
				if ( _parent != null ) { return _parent.root; }

				return null;
			}
		}

		public virtual SceneObject parent {
			get { return _parent; }
		}
		SceneObject _parent = null;

		public virtual int numChildren {
			get { return _children.Count; }
		}

		public virtual bool slideChildren {
			get { return _slideChildren; }
			set { _slideChildren = value; }
		}
		bool _slideChildren = false;
		#endregion



		#region Fields
		List<SceneObject> _children = new List<SceneObject>();
		#endregion



		#region Constructors
		public SceneObject() {
			
		}
		#endregion



		#region Static Methods
		public static Coroutine StartCoroutine( IEnumerator routine ) {
			return UXBehaviour.StartCoroutine( routine );
		}

		public static void StopCoroutine( IEnumerator routine ) {
			UXBehaviour.StopCoroutine( routine );
		}
		#endregion



		#region Methods
		public SceneObject Add( SceneObject child ) {
			if ( child == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Add()", "child" ) ); }
			if ( child is UXSceneManager.StageObject ) { throw new Exception( "" ); }

			if ( child.parent != null ) {
				child.parent.Remove( child );
			}

			child._parent = this;
			_children.Add( child );

			return child;
		}

		public SceneObject Insert( int index, SceneObject child ) {
			if ( child == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Insert()", "child" ) ); }
			if ( child is UXSceneManager.StageObject ) { throw new Exception( "" ); }
			if ( index < 0 || _children.Count < index ) { throw new Exception( "" ); }

			if ( child.parent != null ) {
				child.parent.Remove( child );
			}

			child._parent = this;
			_children.Insert( index, child );

			return child;
		}

		public SceneObject Remove( SceneObject child ) {
			if ( child.parent == null ) { throw new Exception( "" ); }
			if ( child.parent != this ) { throw new Exception( "" ); }

			child._parent = null;
			_children.Remove( child );

			return child;
		}

		public SceneObject RemoveAt( int index ) {
			if ( index < 0 || _children.Count - 1 < index ) { throw new Exception( "" ); }

			SceneObject child = _children[index] as SceneObject;

			child._parent = null;
			_children.Remove( child );

			return child;
		}

		public void RemoveAll() {
			for ( int i = 0; i < _children.Count; i++ ) {
				SceneObject child = _children[i] as SceneObject;

				child._parent = null;
			}

			_children = new List<SceneObject>();
		}

		public SceneObject GetChildByName( string name ) {
			for ( int i = 0; i < _children.Count; i++ ) {
				SceneObject child = _children[i] as SceneObject;

				if ( child.name == name ) { return child; }
			}

			return null;
		}

		public SceneObject GetChildAt( int index ) {
			if ( index < 0 || _children.Count - 1 < index ) { throw new Exception( "" ); }

			return _children[index] as SceneObject;
		}

		public int GetChildIndex( SceneObject child ) {
			if ( child == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetChildIndex()", "child" ) ); }

			for ( int i = 0; i < _children.Count; i++ ) {
				SceneObject child2 = _children[i] as SceneObject;

				if ( child2 == child ) { return i; }
			}

			return -1;
		}

		public void SwapChildren( SceneObject child1, SceneObject child2 ) {
			if ( child1 == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "SwapChildren()", "child1" ) ); }
			if ( child2 == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "SwapChildren()", "child2" ) ); }

			SwapChildrenAt( GetChildIndex( child1 ), GetChildIndex( child2 ) );
		}

		public void SwapChildrenAt( int index1, int index2 ) {
			SceneObject child1 = _children[index1];
			SceneObject child2 = _children[index2];

			if ( child1 == null || child2 == null ) { throw new Exception( "" ); }

			_children[index1] = child2;
			_children[index2] = child1;
		}
		#endregion



		#region EventHandlers
		public virtual IEnumerator OnEnter( SceneStatus status ) {
			yield break;
		}

		public virtual IEnumerator OnEnterOverlapUp() {
			yield break;
		}

		public virtual IEnumerator OnEnterOverlapDown() {
			yield break;
		}

		public virtual IEnumerator OnEnterOverlapSlide() {
			yield break;
		}

		public virtual IEnumerator OnEnterPartUp() {
			yield break;
		}

		public virtual IEnumerator OnEnterPartDown() {
			yield break;
		}

		public virtual IEnumerator OnEnterPartSlide() {
			yield break;
		}

		public virtual IEnumerator OnExit( SceneStatus status ) {
			yield break;
		}

		public virtual IEnumerator OnExitOverlapUp() {
			yield break;
		}

		public virtual IEnumerator OnExitOverlapDown( ) {
			yield break;
		}

		public virtual IEnumerator OnExitOverlapSlide() {
			yield break;
		}

		public virtual IEnumerator OnExitPartUp() {
			yield break;
		}

		public virtual IEnumerator OnExitPartDown() {
			yield break;
		}

		public virtual IEnumerator OnExitPartSlide() {
			yield break;
		}

		public virtual IEnumerator OnArrive() {
			yield break;
		}

		public virtual IEnumerator OnLeave() {
			yield break;
		}
		#endregion
	}
}
