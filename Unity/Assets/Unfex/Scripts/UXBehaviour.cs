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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Unfex.Managers;

[DisallowMultipleComponent]
public abstract class UXBehaviour:MonoBehaviour {

	#region Static Fields
	static Dictionary<GameObject, UXBehaviour> _gameObjectBehaviours = new Dictionary<GameObject, UXBehaviour>();

	static UXManager _uxManager = null;
	#endregion



	#region Properties
	public RectTransform rectTransform {
		get { return transform as RectTransform; }
	}
	#endregion



	#region Fields
	MonoBehaviour _behaviour = null;
	#endregion



	#region Static Methods
	public static UXBehaviour InstantiateTo( UXBehaviour behaviour ) {
		return InstantiateTo( behaviour, default( GameObject ), null );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, object[] args ) {
		return InstantiateTo( behaviour, default( GameObject ), args );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, MonoBehaviour parent ) {
		return InstantiateTo( behaviour, parent.gameObject, null );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, Transform parent ) {
		return InstantiateTo( behaviour, parent.gameObject, null );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, GameObject parent ) {
		return InstantiateTo( behaviour, parent, null );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, MonoBehaviour parent, object[] args ) {
		return InstantiateTo( behaviour, parent.gameObject, args );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, Transform parent, object[] args ) {
		return InstantiateTo( behaviour, parent.gameObject, args );
	}

	public static UXBehaviour InstantiateTo( UXBehaviour behaviour, GameObject parent, object[] args ) {
		if ( behaviour == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "InstantiateTo()", "behaviour" ) ); }
		if ( parent == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "InstantiateTo()", "parent" ) ); }

		GameObject instance = Instantiate( behaviour.gameObject ) as GameObject;

		if ( parent != null ) {
			instance.transform.SetParent( parent.transform );
		}

		instance.transform.localPosition = behaviour.transform.localPosition;
		instance.transform.localRotation = behaviour.transform.localRotation;
		instance.transform.localScale = behaviour.transform.localScale;

		UXBehaviour instanceBehaviour = _gameObjectBehaviours[instance] as UXBehaviour;

		_executeInit( instanceBehaviour, args );

		return instanceBehaviour;
	}

	public static T AddComponent<T>( MonoBehaviour behaviour ) where T:UXBehaviour {
		return AddComponent<T>( behaviour, null );
	}

	public static T AddComponent<T>( MonoBehaviour behaviour, object[] args ) where T:UXBehaviour {
		if ( behaviour == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "AddComponent()", "behaviour" ) ); }

		T instance = AddComponent<T>( behaviour.gameObject, args );
		instance._behaviour = behaviour;

		_executeInit( instance, args );

		return instance;
	}

	public static T AddComponent<T>( GameObject gameObject ) where T:UXBehaviour {
		return AddComponent<T>( gameObject, null );
	}

	public static T AddComponent<T>( GameObject gameObject, object[] args ) where T:UXBehaviour {
		if ( gameObject == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "AddComponent()", "gameObject" ) ); }

		T instance = gameObject.AddComponent<T>();

		_executeInit( instance, args );

		return instance;
	}

	public static UXBehaviour GetInstance( Component component ) {
		if ( component == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetInstance()", "component" ) ); }

		return GetInstance( component.gameObject );
	}

	public static UXBehaviour GetInstance( GameObject gameObject ) {
		if ( gameObject == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetInstance()", "gameObject" ) ); }

		if ( !_gameObjectBehaviours.ContainsKey( gameObject ) ) { return null; }

		return _gameObjectBehaviours[gameObject] as UXBehaviour;
	}

	public static new Coroutine StartCoroutine( IEnumerator routine ) {
		_initialize();

		MonoBehaviour monoBehaviour = _uxManager;

		return monoBehaviour.StartCoroutine( routine );
	}

	public static new void StopCoroutine( IEnumerator routine ) {
		_initialize();

		MonoBehaviour monoBehaviour = _uxManager;

		monoBehaviour.StopCoroutine( routine );
	}

	public static void Trace( params object[] expressions ) {
		string[] messages = new string[expressions.Length];

		for ( int i = 0; i < expressions.Length; i++ ) {
			messages[i] = expressions[i].ToString();
		}

		Debug.Log( string.Join( ", ", messages ) );
	}

	static void _initialize() {
		if ( UXBehaviour._uxManager != null ) { return; }

		GameObject uxManager = new GameObject( "UXManager" );

		UXBehaviour._uxManager = uxManager.AddComponent<UXManager>();

		DontDestroyOnLoad( uxManager );
	}

	static void _executeInit( UXBehaviour behaviour, object[] args ) {
		List<Type> types = new List<Type>();

		if ( args != null ) {
			for ( int i = 0; i < args.Length; i++ ) {
				object arg = args[i];

				if ( arg == null ) { continue; }

				types.Add( arg.GetType() );
			}
		}

		MethodInfo methodInfo = behaviour.GetType().GetMethod( "Init", BindingFlags.Instance|BindingFlags.NonPublic, Type.DefaultBinder, types.ToArray(), null );

		if ( methodInfo != null ) {
			methodInfo.Invoke( behaviour, args );
		} else {
			Debug.LogWarning( string.Format( "Does not exist {0} method with the corresponding argument in class {1}.", "Init", behaviour.GetType().Name ) );
		}
	}
	#endregion



	#region Methods
	protected T GetBehaviour<T>() where T:MonoBehaviour {
		if ( _behaviour != null ) { return (T)_behaviour; }

		return (T)_behaviour;
	}

	public UXManager GetManager() {
		return _uxManager;
	}

	public virtual void Active() {
		gameObject.SetActive( true );
	}

	public virtual void Inactive() {
		gameObject.SetActive( false );
	}
	#endregion



	#region EventHandlers
	protected virtual void Awake() {
		_initialize();

		_gameObjectBehaviours.Add( gameObject, this );
	}

	protected virtual void Init() {

	}

	protected virtual void Start() {
		if ( !_gameObjectBehaviours.ContainsKey( gameObject ) ) {
			gameObject.SetActive( false );

			throw new Exception( "" );
		}
	}

	protected virtual void OnDestroy() {
		if ( _gameObjectBehaviours.ContainsKey( gameObject ) ) {
			_gameObjectBehaviours.Remove( gameObject );
		}
	}
	#endregion
}
