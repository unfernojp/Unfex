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
using LitJson;
using Unfex.Utils;

namespace Unfex.Models {

	[System.Serializable]
	public abstract class JsonModel:Model {

		#region Static Fields
		static bool _initialize = false;
		#endregion



		#region Fields
		JsonData _jsonData = null;

		List<string> _properties = null;
		#endregion



		#region Constructors
		public JsonModel( JsonData jsonData = null ) {
			if ( !_initialize ) {
				JsonMapper.RegisterExporter<float>( ( obj, writer ) => { writer.Write( Convert.ToDouble( obj ) ); } );
				JsonMapper.RegisterImporter<double, float>( ( input ) => { return Convert.ToSingle( input ); } );
				JsonMapper.RegisterImporter<Int32, long>( ( input ) => { return Convert.ToInt64( input ); } );

				_initialize = true;
			}

			_jsonData = jsonData;
			_properties = new List<string>();

			if ( jsonData != null ) {
				JsonReader reader = new JsonReader( jsonData.ToJson() );

				int findDepth = 0;

				while ( reader.Read() ) {
					switch ( reader.Token ) {
						case JsonToken.PropertyName :
							if ( findDepth != 1 ) { continue; }

							_properties.Add( reader.Value.ToString() );
							break;
						case JsonToken.ObjectStart :
						case JsonToken.ArrayStart :
							findDepth++;
							break;
						case JsonToken.ObjectEnd :
						case JsonToken.ArrayEnd :
							findDepth--;
							break;
					}
				}
			}
		}
		#endregion



		#region Methods
		protected bool contains( string name ) {
			return _properties.Contains( name );
		}

		protected T getData<T>( string name ) {
			if ( typeof( T ) == typeof( JsonData ) ) { return (T)(object)_jsonData[name]; }

			return StringUtil.To<T>( _jsonData[name].ToString() );
		}

		public JsonData ToJsonData() {
			JsonData jsonData = new JsonData();

			foreach ( KeyValuePair<string, object> pair in properties ) {
				JsonData value = _parse( pair.Value );

				if ( value == null ) { continue; }

				jsonData[pair.Key] = value;
			}

			return jsonData;
		}

		JsonData _parse( object value ) {
			if ( value is JsonModel ) { return ( value as JsonModel ).ToJsonData(); }

			if ( value is string ) {
				if ( (string)value != "" ) { return new JsonData( (string)value ); }

				return null;
			}

			if ( value is char ) { return new JsonData( (char)value ); }

			if ( value is bool ) { return new JsonData( (bool)value ); }

			if ( value is int ) { return new JsonData( (int)value ); }
			if ( value is float ) { return new JsonData( (float)value ); }
			if ( value is short ) { return new JsonData( (short)value ); }
			if ( value is long ) { return new JsonData( (long)value ); }
			if ( value is double ) { return new JsonData( (double)value ); }
			if ( value is decimal ) { return new JsonData( (decimal)value ); }

			if ( value is IEnumerable ) {
				JsonData jsonData = new JsonData();
				int count = 0;

				foreach( object temp in (IEnumerable)value ) {
					jsonData.Add( _parse( temp ) );
					count++;
				}

				if ( count > 0 ) { return jsonData; }
			}

			return null;
		}

		public string ToJson() {
			return ToJsonData().ToJson();
		}

		public string ToJson( JsonWriter writer ) {
			ToJsonData().ToJson( writer );

			return writer.ToString();
		}
		#endregion
	}
}
