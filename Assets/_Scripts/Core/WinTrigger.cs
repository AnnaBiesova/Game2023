using System;
using _Scripts.Controllers;
using UnityEngine;

namespace _Scripts
{
	public class WinTrigger : MonoBehaviour
	{
		private Collider thisBoxCollider;
        
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerMovement>())
			{
				LevelManager.Instance.SetLevelEnd(true);
			}
		}

		private void OnDrawGizmos()
		{
			thisBoxCollider ??= GetComponent<BoxCollider>();
			
			if(thisBoxCollider == null) return;

			Color gizmosColor = Color.green;
			gizmosColor.a = 0.3f;
            
			Gizmos.color = gizmosColor;
			Gizmos.DrawCube(thisBoxCollider.bounds.center, thisBoxCollider.bounds.size);
		}
	}
}