using UnityEngine;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool
{
	public class PooledObjectWithLifetime : PooledObject
	{
		[Header("Settings")] [SerializeField] private float lifeTime = 2f;
		private float timeToLive;

		protected override void OnEnable()
		{
			UpdateTimeToLive();
			
			base.OnEnable();
		}

		protected virtual void Update()
		{
			if(isPartOfPool == false) return;
            
			if (isPooled == false && Time.time >= timeToLive && resetCallsCounter < 1)
			{
				//Debug.LogWarning($"RESET {resetCallsCounter} on {name} _ {Time.time}", this);
				
				ResetObject();
			}
		}

		protected void UpdateTimeToLive()
		{
			timeToLive = Time.time + lifeTime;
		}
		
		public void SetLifeTime(float lifeTime)
		{
			this.lifeTime = lifeTime;
			UpdateTimeToLive();
		}
	}
}