using System;
using _Scripts.Controllers;
using UnityEngine;

namespace _Scripts
{
	public class LoseCollider : MonoBehaviour
	{
		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent<PlayerMovement>())
			{
				LevelManager.Instance.SetLevelEnd(false);
			}
		}
	}
}