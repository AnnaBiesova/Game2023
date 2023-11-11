using System.Collections.Generic;
using _Scripts.Helpers.CollisinFilterSystem.Interfaces;
using UnityEngine;

namespace _Scripts.Helpers.CollisinFilterSystem
{
	/// <summary>
	/// Remember always create instances of derived classes in Awake, or other methods
	/// for proper clean up of internal collision lists on every game level
	/// </summary>
	public abstract class BaseCollisionFilter : ICollisionFilter
	{
		public abstract bool IsCollisionValid(Collider colliderWeCollide);
		
		public class ColliderComparer : IComparer<Collider>
		{
			public int Compare(Collider x, Collider y)
			{
				//incidentally the hashcode is the instanceid and happens to be unique for all UnityEngine.Objects including Collider2D
				return x.GetHashCode() == y.GetHashCode() ? 0 : 1;
			}
		}
	}
}