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
using System.Collections.Generic;
using UnityEngine;

public class UXDebugger {

	#region Static Properties
	public static string name {
		get { return _name; }
	}
	static string _name = "";

	public static bool enabled {
		get { return _enabled; }
		set { _enabled = value; }
	}
	static bool _enabled = true;

	public static List<string> logs {
		get { return new List<string>( _logs ); }
	}
	static List<string> _logs = new List<string>();
	#endregion



	#region Constructors
	private UXDebugger() {

	}
	#endregion



	#region Methods
	public static void Log( object message ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.Log( message );
	}

	public static void Log( object message, UnityEngine.Object context ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.Log( message, context );
	}

	public static void LogError( object message ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogError( message );
	}

	public static void LogError( object message, UnityEngine.Object context ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogError( message, context );
	}

	public static void LogException( Exception exception ) {
		_logs.Add( exception.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogException( exception );
	}

	public static void LogException( Exception exception, UnityEngine.Object context ) {
		_logs.Add( exception.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogException( exception, context );
	}

	public static void LogWarning( object message ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogWarning( message );
	}

	public static void LogWarning( object message, UnityEngine.Object context ) {
		_logs.Add( message.ToString() );

		if ( !_enabled ) { return; }

		Debug.LogWarning( message, context );
	}
	#endregion
}
