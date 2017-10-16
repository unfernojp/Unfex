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
using System.Text.RegularExpressions;

namespace Unfex.Scenes {

	public sealed class SceneId:IEquatable<SceneId> {

		#region Static Properties
		public static SceneId NaS {
			get { return _NaS; }
		}
		static SceneId _NaS = new SceneId( "NaS" );
		#endregion



		#region Fields
		public string path {
			get { return _path; }
		}
		string _path = "";

		public int length {
			get { return _length; }
		}
		int _length = 0;
		#endregion



		#region Constructors
		public SceneId( string path ) {
			_path = _normalize( path );
			_length = _path.Split( new string[]{ "/" }, StringSplitOptions.None ).Length - 2;
		}

		public SceneId( SceneId sceneId ) {
			_path = sceneId._path;
			_length = sceneId._length;
		}
		#endregion



		#region Static Operators
		public static bool operator ==( SceneId sceneId1, SceneId sceneId2 ) {
			if ( object.ReferenceEquals( sceneId1, sceneId2 ) ) { return true; }
			if ( ( (object)sceneId1 == null) || ( (object)sceneId2 == null ) ) { return false; }

			return ( sceneId1._path == sceneId2._path );
		}

		public static bool operator !=( SceneId sceneId1, SceneId sceneId2 ) {
			return !( sceneId1 == sceneId2 );
		}
		#endregion



		#region Static Methods
		static string _normalize( string path ) {
			if ( path[path.Length - 1].ToString() != "/" ) {
				path += "/";
			}

			while ( path.IndexOf( "//" ) != -1 ) {
				path = String.Join( "/", path.Split( new string[]{ "//" }, StringSplitOptions.None ) );
			}

			path = String.Join( "/", path.Split( new string[]{ "/./" }, StringSplitOptions.None ) );

			while ( new Regex( "/[_a-z0-9]+/\\.\\./", RegexOptions.IgnoreCase ).IsMatch( path ) ) {
				path = Regex.Replace( path, "/[_a-z0-9]+/\\.\\./", "/", RegexOptions.IgnoreCase );
			}

			while ( new Regex( "/\\.\\./?$", RegexOptions.IgnoreCase ).IsMatch( path ) ) {
				path = Regex.Replace( path, "/\\.\\./?$", "/", RegexOptions.IgnoreCase );
			}

			if ( new Regex( "^/([_a-z0-9]+/)*$", RegexOptions.IgnoreCase ).IsMatch( path ) ) { return path; }

			return "NaS";
		}
		#endregion



		#region Methods
		public SceneId Transfer( string path ) {
			string path1 = _path;
			string path2 = path;

			if ( path2[0].ToString() != "/" ) {
				path2 = path1 + path2;
			}

			return new SceneId( path2 );
		}

		public SceneId GetRange( int beginIndex, int endIndex ) {
			string[] segments = _path.Split( new string[]{ "/" }, StringSplitOptions.None );

			if ( segments.Length == 1 ) { return new SceneId( _path ); }

			beginIndex = Math.Max( 0, Math.Min( beginIndex + 1, segments.Length - 1 ) );
			endIndex = Math.Max( 0, Math.Min( endIndex + 1, segments.Length - 1 ) );

			int count = Math.Max( 0, endIndex - beginIndex );

			segments = new List<string>( segments ).GetRange( beginIndex, count ).ToArray();

			return new SceneId( "/" + String.Join( "/", segments ) + "/" );
		}

		public override int GetHashCode() {
			return _path.GetHashCode();
		}

		public override bool Equals( object obj ) {
			SceneId sceneId = obj as SceneId;

			if ( sceneId == null ) { return false; }

			return ( _path == sceneId._path );
		}

		bool IEquatable<SceneId>.Equals( SceneId other ) {
			if ( other == null) { return false; }
			if ( this.path != other.path ) { return false; }

			return true;
		}

		public bool Lines( SceneId sceneId ) {
			if ( _length != sceneId._length ) { return false; }

			string path1 = _path;
			string path2 = sceneId._path;

			if ( path1 == "/" && path2 == "/" ) { return true; }
			if ( path1 == "/" || path2 == "/" ) { return false; }

			string[] segments1 = path1.Split( new string[]{ "/" }, StringSplitOptions.None );
			string[] segments2 = path2.Split( new string[]{ "/" }, StringSplitOptions.None );

			path1 = String.Join( "/", new List<string>( segments1 ).GetRange( 0, segments1.Length - 2 ).ToArray() );
			path2 = String.Join( "/", new List<string>( segments2 ).GetRange( 0, segments1.Length - 2 ).ToArray() );

			return ( path1 == path2 );
		}

		public bool Contains( SceneId sceneId ) {
			string path1 = _path;
			string path2 = sceneId._path;

			if ( path1 == path2 ) { return false; }

			return new Regex( "^" + path1 ).IsMatch( path2 );
		}

		public override string ToString() {
			return _path;
		}
		#endregion
	}
}
