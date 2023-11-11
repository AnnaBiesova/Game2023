using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extensions;
using _Scripts.Patterns.Events;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core
{
	sealed class Pool
	{
		public static Quaternion DefaultQuaternion = new Quaternion(-1f, -1f, -1f, -1f);
		
		public readonly Queue<PooledObject> availableObjQueue = new Queue<PooledObject>();
		private readonly Vector3 poolObjectsPos = new Vector3(100f, 100f, 100f);
		private readonly bool fixedSize;
		private readonly List<PooledObject> poolObjectPrefabs;
		private readonly string poolName;
		private readonly Transform parentForPooledObjects;
		
		private int poolSize = 0;
		private Transform parent;

		private event Action<PooledObject> OnAddNewElementToPool; 

		public Pool(string poolName, PooledObject[] poolObjectPrefabs, int initialCount, bool fixedSize, Transform parentForPooledObjects = null)
		{
			this.poolName = poolName;
			this.poolObjectPrefabs = poolObjectPrefabs.ToList();
			this.fixedSize = fixedSize;
			this.parentForPooledObjects = parentForPooledObjects;
			
			this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);
			
			//populate the pool
			for(int index = 0; index < initialCount; index++) {
				AddObjectToPoolInternal(NewObjectInstance());
			}
			
			if(this.poolObjectPrefabs.Count > 1) ShuffleQueue();
		}

		private void OnLevelDone()
		{
			OnAddNewElementToPool = null;
		}
		
		public void SubscribeToAddNewElement(Action<PooledObject> callback)
		{
			OnAddNewElementToPool += callback;
		}

		//o(1)
		private PooledObject AddObjectToPoolInternal(PooledObject po) 
		{
			po.transform.SetParent(parent);
			po.gameObject.SetActive(false);
			po.transform.position = poolObjectsPos;
			availableObjQueue.Enqueue(po);
			
			po.isPooled = true;

			//Debug.LogWarning($"Object: {po.name} returned to pool: {poolName} status pulled: {po.isPooled} ___ {Time.time}");
			
			return po;
		}

		private void ShuffleQueue()
		{
			List<PooledObject> temp = availableObjQueue.ToList();
			temp.Shuffle();

			availableObjQueue.Clear();

			foreach (var pooledObject in temp)
			{
				availableObjQueue.Enqueue(pooledObject);
			}
		}

		public void AddObjectToPool(PooledObject po, int instancesCount = 1)
		{
			poolObjectPrefabs.Add(po);

			//populate the pool
			for (int index = 0; index < instancesCount; index++)
			{
				AddObjectToPoolInternal(NewObjectInstance());
			}

			if (poolObjectPrefabs.Count > 1) ShuffleQueue();
		}
		
		private PooledObject NewObjectInstance() 
		{
			//create parent
			if (parent == null)
			{
				parent = new GameObject(poolName + "_Pool").transform;
				parent.SetParent(parentForPooledObjects != null ? parentForPooledObjects : null);
				parent.localPosition = Vector3.zero;
				parent.localRotation = Quaternion.identity;
			}
			
			PooledObject pooledObject = Object.Instantiate(poolObjectPrefabs[poolSize % poolObjectPrefabs.Count], parent);
			pooledObject.enabled = true;
			pooledObject.poolName = poolName;
			pooledObject.SetInitialIndexInPool(poolSize);
			
			this.poolSize++;
            
			OnAddNewElementToPool?.Invoke(pooledObject);
            
			return pooledObject;
		}

		//o(1)
		public T NextAvailableObject<T>(Vector3 position, Quaternion rotation) where T : PooledObject {
			PooledObject result = null;

			if (availableObjQueue.Count > 0)
			{
				result = availableObjQueue.Dequeue();
			} 
			else if(fixedSize == false) 
			{
				result = NewObjectInstance();
			}
			else
			{
				return null;
			}
			
			result.isPooled = false;

			result.transform.SetPositionAndRotation(position,
				rotation == Quaternion.identity ? result.transform.rotation : rotation);
			
			result.gameObject.SetActive(true);
			
			return result as T;
		} 
		
		//o(1)
		public void ReturnObjectToPool(PooledObject po) {
			
			if(poolName.Equals(po.poolName)) 
			{
				/* we could have used availableObjStack.Contains(po) to check if this object is in pool.
				 * While that would have been more robust, it would have made this method O(n) 
				 */
				if(po.isPooled) 
				{
					Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");	
				} 
				else 
				{
					AddObjectToPoolInternal(po);
				}
			}
		}
	}
}