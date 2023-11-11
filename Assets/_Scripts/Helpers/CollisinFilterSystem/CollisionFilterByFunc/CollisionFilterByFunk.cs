using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Helpers.CollisinFilterSystem.CollisionFilterByFunc
{
	public class CollisionFilterByFunk : BaseCollisionFilter
	{
		protected static HashSet<Collider> correctColliders = new HashSet<Collider>();
		protected static HashSet<Collider> wrongColliders = new HashSet<Collider>();

		protected Func<bool, Collider> testFunc;

		public CollisionFilterByFunk(Func<bool, Collider> testFunc)
		{
			this.testFunc = testFunc;

			correctColliders.Clear();
			wrongColliders.Clear();
		}

		public override bool IsCollisionValid(Collider colliderWeCollide)
		{
			if (wrongColliders.Contains(colliderWeCollide)) return false;

			if (correctColliders.Contains(colliderWeCollide))
			{
				return true;
			}

			if (testFunc(colliderWeCollide))
			{
				correctColliders.Add(colliderWeCollide);
				return true;
			}

			wrongColliders.Add(colliderWeCollide);
			return false;
		}
	}
}