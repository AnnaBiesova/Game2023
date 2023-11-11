using System;
using System.Collections;
using UnityEngine;

namespace _Scripts.Extensions
{

    
	public static class GameObjectExtensions
	{

		
		public static Coroutine StartCoroutine(this GameObject gameObject, IEnumerator routine)
		{
		 	 return gameObject.AddComponent<RoutineOwner>().StartCoroutine(routine);
		}
	}
}