using UnityEngine;

namespace _Scripts.Helpers.CollisinFilterSystem.Interfaces
{
	public interface ICollisionFilter
	{
		public bool IsCollisionValid(Collider colliderWeCollide);
	}
}