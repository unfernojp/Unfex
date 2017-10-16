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
using System.IO;
using System.Text;
using LitJson;
using UnityEngine;

namespace Unfex.IO {

	public class DataLoader {

		#region Fields
		public enum Protocol { File, Resource }

		public enum Type { Text, Json, XML }
		#endregion



		#region Constructors
		public DataLoader() {

		}
		#endregion



		#region Methods
		public static Result Load( string path ) {
			return Load( path, DataLoader.Protocol.File, DataLoader.Type.Text, false );
		}

		public static Result Load( string path, DataLoader.Protocol protocol ) {
			return Load( path, protocol, DataLoader.Type.Text, false );
		}

		public static Result Load( string path, DataLoader.Protocol protocol, DataLoader.Type type ) {
			return Load( path, protocol, type, false );
		}

		public static Result Load( string path, DataLoader.Protocol protocol, DataLoader.Type type, bool encrypt ) {
			object data = null;
			Exception errorObject = null;

			switch ( protocol ) {
				case DataLoader.Protocol.File :
					#if UNITY_WEBPLAYER
					throw new Exception( "File プロトコルは Web Player では使用できません。" );
					#else
					try {
						StreamReader reader = new StreamReader( path, Encoding.UTF8 );

						data = reader.ReadToEnd();

						reader.Close();
					} catch ( Exception exception ) {
						data = null;
						errorObject = exception;
					}
					#endif
					break;
				case DataLoader.Protocol.Resource :
					try {
						TextAsset text = Resources.Load<TextAsset>( path );

						if ( text != null ) {
							data = text.text;
						} else {
							errorObject = new Exception( "リソース " + path + " は存在しません。" );
						}
					} catch ( Exception exception ) {
						data = null;
						errorObject = exception;
					}
					break;
			}

			if ( data != null ) {
				switch ( type ) {
					case DataLoader.Type.Text :
						break;
					case DataLoader.Type.Json :
						try {
							data = JsonMapper.ToObject( data.ToString() );
						} catch ( Exception exception ) {
							data = null;
							errorObject = exception;
						}
						break;
					case DataLoader.Type.XML :
						break;
				}
			}

			return new Result( data, errorObject );
		}

		public static Result Save( string path, string data ) {
			Exception errorObject = null;

			try {
				string directoryPath = "";
				string[] segments = path.Split( new string[]{ "/" }, StringSplitOptions.None );

				for ( int i = 0; i < segments.Length - 1; i++ ) {
					directoryPath += segments[i] + "/";
				}

				if ( !Directory.Exists( directoryPath ) ) {
					Directory.CreateDirectory( directoryPath );
				}

				File.WriteAllText( path, data, Encoding.UTF8 );
			} catch ( Exception exception ) {
				errorObject = exception;
			}

			return new Result( data, errorObject );
		}

		public static bool Exists( string path ) {
			return File.Exists( path );
		}
		#endregion



		#region Classes
		public class Result {

			#region Properties
			public object data {
				get { return _data; }
			}
			protected object _data = null;

			public Exception errorObject {
				get { return _errorObject; }
			}
			protected Exception _errorObject = null;
			#endregion



			#region Constructors
			public Result( object data = null, Exception errorObject = null ) {
				_data = data;
				_errorObject = errorObject;
			}
			#endregion
		}
		#endregion
	}
}
