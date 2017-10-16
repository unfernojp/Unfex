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
using UnityEngine;

public class UXStarter:MonoBehaviour {

	#region EventHandlers
	void Start() {
		if ( transform.childCount > 0 ) { throw new Exception( "スターターに子オブジェクトが登録されている状態では起動できません" ); }

		OnStart();

		Destroy( gameObject );
	}

	protected virtual void OnStart() {

	}
	#endregion
}
