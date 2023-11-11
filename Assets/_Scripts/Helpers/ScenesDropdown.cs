using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace _Scripts.Helpers
{
	public static class ScenesDropdown
	{
		public static List<string> GetScenesInBuildSettings()
		{
			List<string> scenesList = new List<string>();

			for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
			{
				scenesList.Add(GetSceneNameFromIndex(i, false));
			}

			return scenesList;
		}

		public static string GetSceneNameFromIndex(int BuildIndex, bool fullPath = true)
		{
			string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);

			if (fullPath) return path;

			int slash = path.LastIndexOf('/');
			string name = path.Substring(slash + 1);
			int dot = name.LastIndexOf('.');
			return name.Substring(0, dot);
		}
	}
}