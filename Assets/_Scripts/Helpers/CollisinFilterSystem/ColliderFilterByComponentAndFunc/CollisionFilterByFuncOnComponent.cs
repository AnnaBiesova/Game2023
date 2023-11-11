using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Helpers.CollisinFilterSystem
{
	/// <summary>
	/// Remember always create instances of derived classes in Awake, or other methods
	/// for proper clean up of internal collision lists on every game level
	/// </summary>
	public class CollisionFilterByFuncOnComponent<T> : BaseCollisionFilter where T: Component
	{
		protected static Dictionary<Collider, T> correctColliders = new Dictionary<Collider, T>();
		protected static HashSet<Collider> wrongColliders = new HashSet<Collider>();

		protected Func<T, bool> testFunc;

		protected static T LastTestedObject;
		
		public CollisionFilterByFuncOnComponent(Func<T, bool> testFunc)
		{
			this.testFunc = testFunc;
			
			correctColliders.Clear();
			wrongColliders.Clear();
		}

		public override bool IsCollisionValid(Collider colliderWeCollide)
		{
			if (wrongColliders.Contains(colliderWeCollide)) return false;

			if (correctColliders.TryGetValue(colliderWeCollide, out T component))
			{
				bool funkResult = testFunc(component);
				if (funkResult) LastTestedObject = component;
				return funkResult;
			}

			if (colliderWeCollide.TryGetComponent(out T tryGetComponent))
			{
				correctColliders.Add(colliderWeCollide, tryGetComponent);
				bool funkResult = testFunc(tryGetComponent);
				if (funkResult) LastTestedObject = tryGetComponent;
				return funkResult;
			}
			
			wrongColliders.Add(colliderWeCollide);
			return false;
		}
		

		public T GetLastTestedObject()
		{
			return LastTestedObject;
		}
	}
}