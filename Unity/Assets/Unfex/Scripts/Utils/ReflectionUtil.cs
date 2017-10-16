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
using System.Reflection;

namespace Unfex.Utils {

	public sealed class ReflectionUtil {

		#region Static Methods
		public static object GetProperty( object target, string name ) {
			return GetProperty( target, name, default( BindingFlags ) );
		}

		public static object GetProperty( object target, string name, BindingFlags bindingFlags ) {
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetProperty()", "target" ) ); }

			PropertyInfo propertyInfo = target.GetType().GetProperty( name, bindingFlags );

			if ( propertyInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Property", name ) ); }

			MethodInfo methodInfo = propertyInfo.GetGetMethod();

			if ( methodInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Property", name ) ); }

			return methodInfo.Invoke( target, null );
		}

		public static void SetProperty( object target, string name, object value ) {
			SetProperty( target, name, value, default( BindingFlags ) );
		}

		public static void SetProperty( object target, string name, object value, BindingFlags bindingFlags ) {
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "SetProperty()", "target" ) ); }

			PropertyInfo propertyInfo = target.GetType().GetProperty( name, bindingFlags );

			if ( propertyInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Property", name ) ); }

			MethodInfo methodInfo = propertyInfo.GetSetMethod();

			if ( methodInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Property", name ) ); }

			methodInfo.Invoke( target, new object[]{ value } );
		}

		public static object GetField( object target, string name ) {
			return GetField( target, name, default( BindingFlags ) );
		}

		public static object GetField( object target, string name, BindingFlags bindingFlags ) {
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "GetField()", "target" ) ); }

			FieldInfo fieldInfo = target.GetType().GetField( name, bindingFlags );

			if ( fieldInfo != null ) {
				return fieldInfo.GetValue( target );
			} else {
				throw new Exception( string.Format( "{0} {1} is not defined.", "Field", name ) );
			}
		}

		public static void SetField( object target, string name, object value ) {
			SetProperty( target, name, value, default( BindingFlags ) );
		}

		public static void SetField( object target, string name, object value, BindingFlags bindingFlags ) {
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "SetField()", "target" ) ); }

			FieldInfo fieldInfo = target.GetType().GetField( name, bindingFlags );

			if ( fieldInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Field", name ) ); }

			fieldInfo.SetValue( target, value );
		}

		public static object Invoke( object target, string name ) {
			return Invoke( target, name, new Type[]{}, new object[]{}, default( BindingFlags ) );
		}

		public static object Invoke( object target, string name, BindingFlags bindingFlags ) {
			return Invoke( target, name, new Type[]{}, new object[]{}, bindingFlags );
		}

		public static object Invoke( object target, string name, object[] args ) {
			return Invoke( target, name, new Type[]{}, args, default( BindingFlags ) );
		}

		public static object Invoke( object target, string name, object[] args, BindingFlags bindingFlags ) {
			return Invoke( target, name, new Type[]{}, args, bindingFlags );
		}

		public static object Invoke( object target, string name, Type[] types, object[] args ) {
			return Invoke( target, name, types, args, default( BindingFlags ) );
		}

		public static object Invoke( object target, string name, Type[] types, object[] args, BindingFlags bindingFlags ) {
			if ( target == null ) { throw new Exception( string.Format( "Argument {1} of method {0} can not be omitted.", "Invoke()", "target" ) ); }

			MethodInfo methodInfo = target.GetType().GetMethod( name, bindingFlags, Type.DefaultBinder, types, null );

			if ( methodInfo == null ) { throw new Exception( string.Format( "{0} {1} is not defined.", "Method", name ) ); }

			return methodInfo.Invoke( target, args );
		}
		#endregion
	}
}
