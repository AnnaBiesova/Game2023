using System;
using System.Collections.Generic;
using _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool
{
	public class PooledObject : MonoBehaviour
	{
		[SerializeField] protected bool isPartOfPool = true;
		[SerializeField] private bool justDisableOnReset; 
		

		public string poolName;

		public bool isPooled;

		protected int resetCallsCounter;
        
		public int InitialIndexInPool { get; private set; }

		public static IEnumerable<string> GetAllPoolsNames()
		{
			return PoolsNames.GetAllPoolsNames();
		}

		protected virtual void OnEnable()
		{
			resetCallsCounter = 0;
		}

		public virtual void ResetObject()
		{
			if (isPartOfPool == false ) return;

			resetCallsCounter++;
			
			if (resetCallsCounter > 1)
			{
				//Debug.LogWarning($"pooled object: {name} resetCallsCounter = {resetCallsCounter} _ {Time.time}", this);
				return;
			}

			if (justDisableOnReset)
			{
				gameObject.SetActive(false);
			}
			else
			{
				Core.EasyObjectPool.Instance.ReturnObjectToPool(this);
			}
		}

		public void SetInitialIndexInPool(int index)
		{
			InitialIndexInPool = index;
		}
	}
}