using UnityEngine;

namespace _Scripts.Utils.NonAllocRaycaster
{
	public static class NonAllocRaycaster
	{
		private static readonly RaycastHit[] lastHits = new RaycastHit[1];
		private static RaycastHit lastHit;
		
		public static RaycastHit GetLastHit() => lastHits[0];

		public static bool Raycast(Ray ray, float maxDist, LayerMask layerMask)
		{
			if (Physics.RaycastNonAlloc(ray, lastHits, maxDist, layerMask) > 0)
			{
				return true;
			}

			return false;
		}

		public static bool Raycast(Vector3 origin, Vector3 dir, float maxDist, LayerMask layerMask)
		{
			if (Physics.RaycastNonAlloc(origin, dir, lastHits, maxDist, layerMask) > 0)
			{
				return true;
			}

			return false;
		}
	}
}