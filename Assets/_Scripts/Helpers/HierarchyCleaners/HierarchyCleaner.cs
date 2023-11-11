using UnityEngine;

namespace _Scripts.Helpers
{
	public class HierarchyCleaner : MonoBehaviour
	{
		private void Clear()
		{
			//RemoveAllComponents<CharacterBodyPart>();
		}
		
		private void RemoveAllComponents<T>(T typeToRemove = null) where T : UnityEngine.Object
		{
			foreach (T childObject in GetComponentsInChildren<T>())
			{
				DestroyImmediate(childObject);
			}
		}

	}
}