using System;
using _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs;
using UnityEditor;

namespace _Scripts.Controllers.Rules
{
	[Serializable]
	public class RulesInfoBase
	{
		protected BaseLevelConfig baseLevelConfig;

		public void Init(BaseLevelConfig baseLevelConfig)
		{
			this.baseLevelConfig = baseLevelConfig;

#if UNITY_EDITOR
			AssemblyReloadEvents.beforeAssemblyReload += OnDestroyRule;
			Selection.selectionChanged += OnEditorSelectionChanged;
#endif
		}

		public virtual void OnDestroyRule()
		{
#if UNITY_EDITOR
			AssemblyReloadEvents.beforeAssemblyReload -= OnDestroyRule;
			Selection.selectionChanged -= OnEditorSelectionChanged;
#endif
		}

		private void OnEditorSelectionChanged()
		{
#if UNITY_EDITOR
			if (Selection.activeObject == baseLevelConfig)
			{
				SceneView.duringSceneGui -= OnSelectedInEditor;
				SceneView.duringSceneGui += OnSelectedInEditor;
			}
			else
			{
				SceneView.duringSceneGui -= OnSelectedInEditor;
			}
#endif
		}

		protected virtual void OnSelectedInEditor(
#if UNITY_EDITOR
			SceneView sceneView
#endif
		)
		{
			//Debug.LogWarning(baseLevelConfig.name);
		}
	}
}