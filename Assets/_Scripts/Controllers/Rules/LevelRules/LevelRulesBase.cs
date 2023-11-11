using System;
using _Configs.ScriptableObjectsDeclarations.Configs.GameModesConfigs;
using UnityEngine;

namespace _Scripts.Controllers.Rules
{
	public abstract class LevelRulesBase : MonoBehaviour
	{
		public GameObject LevelRulePrefab => gameObject;
		
		public abstract GameModeConfig RuleConfig { get; }
		public abstract Type RulesInfoType { get; }
		public abstract LevelProgressInfo LevelProgressInfo { get; protected set; }

		public abstract void SetLevelProgressInfo();

		public abstract bool CheckWinCondition();

		public abstract void SetRules(RulesInfoBase info);
		
		public abstract int GetCurrentLevelStage();
	}
}