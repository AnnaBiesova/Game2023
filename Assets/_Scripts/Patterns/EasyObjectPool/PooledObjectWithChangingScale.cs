using UnityEngine;

namespace _Scripts.MonoBehaviours.Patterns.EasyObjectPool
{
	public class PooledObjectWithChangingScale : PooledObject
	{
		private Vector3 defaultScale;
        
		protected override void OnEnable()
		{
			defaultScale = transform.localScale;
			
			base.OnEnable();
		}

		public override void ResetObject()
		{
			transform.localScale = defaultScale;
			
			base.ResetObject();
		}
	}
}