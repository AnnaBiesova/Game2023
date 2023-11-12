using System.Linq;
using _Configs;
using UnityEngine;

namespace _Scripts.Controllers.Rules.AiIdle
{
	public class JumpRemakeRule : LevelRules<JumpRemakeRuleInfo>
	{
		private int totalLevelUpgradeTarget;
		private int currentSkillUpgradesCount;
        
		public override bool CheckWinCondition()
		{
			return false;
		}

		public override void SetLevelProgressInfo()
		{
			LevelProgressInfo.levelProgressInSeconds += Time.fixedDeltaTime;
			
			float progress = Mathf.Clamp01((float)currentSkillUpgradesCount / totalLevelUpgradeTarget);
			LevelProgressInfo.SetLinearProgress(progress);
		}

		public override void SetRules(RulesInfoBase info)
		{
			base.SetRules(info);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}