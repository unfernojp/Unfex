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
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Unfex.Utils {

	public sealed class StringUtil {

		#region Static Methods
		public static T To<T>( string value ) {
			if ( typeof( T ) == typeof( string ) ) { return (T)(object)value; }
			if ( typeof( T ) == typeof( char ) ) { return (T)(object)char.Parse( value ); }

			if ( typeof( T ) == typeof( bool ) ) { return (T)(object)bool.Parse( value ); }

			if ( typeof( T ) == typeof( int ) ) { return (T)(object)int.Parse( value ); }
			if ( typeof( T ) == typeof( float ) ) { return (T)(object)float.Parse( value ); }

			if ( typeof( T ) == typeof( short ) ) { return (T)(object)short.Parse( value ); }
			if ( typeof( T ) == typeof( long ) ) { return (T)(object)long.Parse( value ); }
			if ( typeof( T ) == typeof( double ) ) { return (T)(object)double.Parse( value ); }
			if ( typeof( T ) == typeof( decimal ) ) { return (T)(object)decimal.Parse( value ); }

			return default( T );
		}

		public static string ToPascal( string text ) {
			string result = "";

			if ( text.Length > 0 ) {
				result += char.ToUpper( text[0] );
				result += text.Substring( 1 );
			}

			return result;
		}

		public static Color ToColor( string colorText ) {
			Regex reg = new Regex( "^#([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})?$", RegexOptions.IgnoreCase );

			Match match = reg.Match( colorText );

			if ( match.Length == 0 ) { throw new Exception( "カラーコード " + colorText + " は不正な値です。" ); }

			float r = (float)int.Parse( match.Groups[1].Value, NumberStyles.HexNumber ) / 255;
			float g = (float)int.Parse( match.Groups[2].Value, NumberStyles.HexNumber ) / 255;
			float b = (float)int.Parse( match.Groups[3].Value, NumberStyles.HexNumber ) / 255;
			float a = 1.00f;

			if ( match.Length == 9 ) {
				a = (float)int.Parse( match.Groups[4].Value, NumberStyles.HexNumber ) / 255;
			}

			return new Color( r, g, b, a );
		}

		public static string GetFolderPath( string filePath ) {
			string[] segments = filePath.Split( new string[]{ "/", "\\"  }, StringSplitOptions.None );

			if ( segments.Length == 0 ) { return ""; }

			return string.Join( "/", segments, 0, segments.Length - 1 );
		}

		public static string GetFileName( string filePath ) {
			string[] segments = filePath.Split( new string[]{ "/", "\\"  }, StringSplitOptions.None );

			if ( segments.Length == 0 ) { return ""; }

			string fileName = segments[segments.Length - 1];

			segments = fileName.Split( new string[]{ "." }, StringSplitOptions.None );

			return string.Join( ".", segments, 0, segments.Length - 1 );
		}

		public static string GetExtention( string filePath ) {
			string[] segments = filePath.Split( new string[]{ "." }, StringSplitOptions.None );

			if ( segments.Length == 0 ) { return ""; }

			return segments[segments.Length - 1];
		}

		public static string UnescapeUTF8( string text ) {
			Regex reg = new Regex( "(\\\\u([0-9a-fA-F]{4}))" );

			text = reg.Replace( text, new MatchEvaluator( _unescapeUTF8MatchEvaluator ) );

			return text;
		}

		static string _unescapeUTF8MatchEvaluator( Match match ) {
			int int16 = Convert.ToInt32( match.Groups[2].ToString(), 16 );
			char char16 = Convert.ToChar( int16 );

			return char16.ToString();
		}

		public static string removeTag( string str ) {
			str = new Regex( "<(\"[^\"]*\"|'[^']*'|[^'\">])*>", RegexOptions.IgnoreCase | RegexOptions.Singleline ).Replace( str, "" );

			return str;
		}
		#endregion
	}
}
