/*
 * Unless otherwise licensed, this file cannot be copied or redistributed in any format without the explicit consent of the author.
 * (c) Preet Kamal Singh Minhas, http://marchingbytes.com
 * contact@marchingbytes.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Patterns;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core {
    
	/// <summary>
	/// Easy object pool.
	/// </summary>
	public sealed class EasyObjectPool : Singleton<EasyObjectPool>
	{
		public List<PoolInfo> poolInfo;

		
		public List<PoolInfo> poolsCreatedInRuntime;
		
		
#if UNITY_EDITOR
		
		
		
#endif
		
		//mapping of pool name vs list
		private Dictionary<string, Pool> poolDictionary  = new Dictionary<string, Pool>();
		
		// Use this for initialization
		void Awake () {
			//check for duplicate names
			CheckForDuplicatePoolNames();
			//create pools
			CreatePoolsFromEditorData();
		}

		public bool IsPoolPresented(String poolName)
		{
			return poolDictionary.ContainsKey(poolName);
		}
		
		private void CheckForDuplicatePoolNames() 
		{
			for (int index = 0; index < poolInfo.Count; index++) {
				string poolId = poolInfo[index].poolId;
				if(String.IsNullOrEmpty(poolId)) {
					Debug.LogError(string.Format("Pool {0} does not have a name!",index));
				}
				for (int internalIndex = index + 1; internalIndex < poolInfo.Count; internalIndex++) {
					if(poolId.Equals(poolInfo[internalIndex].poolId, StringComparison.InvariantCultureIgnoreCase)) {
						Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
					}
				}
			}
		}

		private void CreatePoolsFromEditorData() 
		{
			foreach (PoolInfo currentPoolInfo in poolInfo)
			{
				CreatePool(currentPoolInfo);
			}
		}

		public void AddNewObjectToPool(string poolID, int instancesCount = 1, params PooledObject[] poolPrefabs)
		{
			if (poolDictionary.ContainsKey(poolID) == false)
			{
				Debug.LogError($"Can't add object ot pool {poolID} __ pool is not presented");
				return;
			}

			foreach (PooledObject pooledObject in poolPrefabs)
			{
				poolDictionary[poolID].AddObjectToPool(pooledObject, instancesCount);
			}
		}

		public void CreateNewPoolInRuntime(string poolID, int poolSize, bool fixedSize, Transform parentForPooledObjects, params PooledObject[] poolPrefabs)
		{
			if (poolDictionary.ContainsKey(poolID))
			{
				Debug.LogError($"Pool as already created, attempt to create second with same name: {poolID}");
				return;
			}
            
			PoolInfo newPoolInfo = new PoolInfo()
			{
				fixedSize = fixedSize, poolId = poolID, 
				poolSize = poolSize, prefabs = poolPrefabs, parentForPooledObjects = parentForPooledObjects
			};
			
			poolsCreatedInRuntime.Add(newPoolInfo);
			
			CreatePool(newPoolInfo);
		}

		private void CreatePool(PoolInfo info)
		{
			var pool = new Pool(info.poolId, info.prefabs,
				info.poolSize, info.fixedSize, info.parentForPooledObjects);

			//add to mapping dict
			poolDictionary[info.poolId] = pool;
		}

		public IEnumerable<T> GetAllObjectsInPool<T>(string poolId) where T : PooledObject
		{
			return poolDictionary[poolId].availableObjQueue.Cast<T>().Reverse();
		}

		public void SubscribeToAddNewObjectCallback(string poolId, Action<PooledObject> callback)
		{
			poolDictionary[poolId].SubscribeToAddNewElement(callback);
		}

		public T GetObjectFromPool<T>(string poolName, Vector3 position, Quaternion rotation) where T : PooledObject
		{
			if(poolDictionary.ContainsKey(poolName)) 
			{
				return poolDictionary[poolName].NextAvailableObject<T>(position, rotation);
			} 
			
			Debug.LogError("Invalid pool name specified: " + poolName);
			return null;
		}
		
		public void ReturnObjectToPool(PooledObject poolObject)
		{
			if(poolDictionary.TryGetValue(poolObject.poolName, out Pool pool)) 
			{
				pool.ReturnObjectToPool(poolObject);
			}
			else
			{ 
				Debug.LogError($"Object {poolObject.name} is not presented in pools with pool ID {poolObject.poolName}");
			}
		}
	}
}
