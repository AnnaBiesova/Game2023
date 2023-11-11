using System.Linq;
using _Configs;
using UnityEngine;

namespace _Scripts.Controllers.Rules.AiIdle
{
	public class AiIdleLevelRule : LevelRules<AiIdleRuleInfo>
	{
		private int totalLevelUpgradeTarget;
		private int currentSkillUpgradesCount;
        
		public override bool CheckWinCondition()
		{
			bool levelCompleted = true;

			for (var i = 0; i < info.LevelCompletionTargets.Count; i++)
			{
				AiIdleRuleInfo.LevelCompletionTarget targetForLevel = info.LevelCompletionTargets[i];


			}

			return levelCompleted;
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

			
			if (LevelManager.LevelIsRestarted == false && LevelManager.Instance.IsHubScene == false)
			{
				ResetPlayerLevelAndMoneyData();
			}


			
			totalLevelUpgradeTarget = base.info.LevelCompletionTargets.Sum(t => t.neededLevel) - 3;


		}

		protected override void OnDestroy()
		{
			base.OnDestroy();


		}

		private void OnPlayerSkillUpgraded(AiIdleLevelRule thisObj, int currentLevel)
		{
			int sum = 0;

			foreach (var levelTarget in info.LevelCompletionTargets)
			{
			}

			currentSkillUpgradesCount = sum - 3;
		}
		
		private void ResetPlayerLevelAndMoneyData()
		{


			SaveManager.ChangePlayerMoney(int.MinValue, false);
		}
	}
}