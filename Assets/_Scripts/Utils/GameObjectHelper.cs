using _Scripts.Extensions;
using UnityEngine;
using Collider = UnityEngine.Collider;

namespace _Scripts.MonoBehaviours.Utils
{
	public class GameObjectHelper : MonoBehaviour
	{
		/*[Button]
		private void ProcessGameObject()
		{
			RemoveAllAnimators();
			RemoveAllRenderers();
			RemoveAllParticleSystems();
			SwitchCollidersToECS();
			DestroyClearObjects();
		}*/

		public void RemoveAllColliders()
		{
			transform.RemoveAllComponentsFromChilds<Collider>();
		}

		private void DestroyClearObjects()
		{
			transform.ProcessChildTransforms(DestroyObjectIfClear);
		}

		private void DestroyObjectIfClear(Transform t)
		{
			if(t.GetComponents<Component>().Length == 1 && t.childCount == 0) DestroyImmediate(t.gameObject);
		}
        
		private void RemoveAllRenderers()
		{
			transform.RemoveAllComponentsFromChilds<Renderer>();			
			transform.RemoveAllComponentsFromChilds<MeshFilter>();			
		}

		private void RemoveAllAnimators()
		{
			transform.RemoveAllComponentsFromChilds<Animator>();
		}

		private void RemoveAllParticleSystems()
		{
			transform.RemoveAllComponentsFromChilds<ParticleSystem>();
		}

		}
}