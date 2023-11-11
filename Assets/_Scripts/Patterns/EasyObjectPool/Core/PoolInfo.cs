using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core
{
	[System.Serializable] 
	public class PoolInfo
	{
		
		public string poolId;
		public PooledObject[] prefabs;
		public int poolSize;
		public bool fixedSize;
		public Transform parentForPooledObjects;

		public static IEnumerable<string> GetAllPoolsNames()
		{
			return PoolsNames.GetAllPoolsNames();
		}
	}
}