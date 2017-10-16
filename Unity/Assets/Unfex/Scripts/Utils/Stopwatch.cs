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

using System.Collections.Generic;
using UnityEngine;

namespace Unfex.Utils {

	public sealed class Stopwatch {

		#region Static Fields
		static float _startTime = 0;

		static Dictionary<string, float> _nameTimes = new Dictionary<string, float>();
		#endregion



		#region Static Methods
		public static void Start() {
			Debug.Log( "Stopwatch : 計測開始" );

			_startTime = Time.time;
		}

		public static void Start( string name ) {
			Debug.Log( "Stopwatch : 計測開始 name=" + name );

			_nameTimes[name] = Time.time;
		}

		public static float Stop() {
			float stopTime = Time.time - _startTime;

			Debug.Log( "Stopwatch : 計測完了 time=" + stopTime + " 秒" );

			return stopTime;
		}

		public static float Stop( string name ) {
			if ( !_nameTimes.ContainsKey( name ) ) { return -1; }

			float stopTime = Time.time - _nameTimes[name];

			Debug.Log( "Stopwatch : 計測完了 name=" + name + " time=" + stopTime + " 秒" );

			return stopTime;
		}

		public static void Reset() {
			_startTime = Time.time;
		}

		public static void Reset( string name ) {
			_nameTimes[name] = Time.time;
		}
		#endregion
	}
}