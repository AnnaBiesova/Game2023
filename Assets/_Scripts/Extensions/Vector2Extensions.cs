using UnityEngine;

namespace _Scripts.Extensions
{
	public static class Vector2Extensions
	{
		//TODO: TEST IMPLEMENTATION PERFORMANCE WITH IN KEYWORD AMD WITHOUT
		public static float GetRandomInRange(this in Vector2 range)
		{
			return Random.Range(range.x, range.y);
		}
	}
}