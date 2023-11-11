using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Helpers.CollisinFilterSystem
{
	/// <summary>
	/// Remember always create instances of derived classes in Awake, or other methods
	/// for proper clean up of internal collision lists on every game level
	/// </summary>
	public class CollisionFilterByComponent<T> : BaseCollisionFilter
	{
		protected static List<Collider> correctColliders = new List<Collider>();
		protected static List<Collider> wrongColliders = new List<Collider>();

		
		public CollisionFilterByComponent()
		{
			correctColliders.Clear();
			wrongColliders.Clear();
		}

		public override bool IsCollisionValid(Collider colliderWeCollide)
		{
			if (wrongColliders.Contains(colliderWeCollide)) return false;
			if (correctColliders.Contains(colliderWeCollide)) return true;
			
			if (colliderWeCollide.GetComponent<T>() != null)
			{
				correctColliders.Add(colliderWeCollide);
				return true;
			}

			wrongColliders.Add(colliderWeCollide);
			return false;
		}
	}
}