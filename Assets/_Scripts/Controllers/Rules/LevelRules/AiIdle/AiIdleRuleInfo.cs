using System;
using System.Collections.Generic;

namespace _Scripts.Controllers.Rules.AiIdle
{
	[Serializable]
	public class AiIdleRuleInfo : RulesInfoBase
	{
		[Serializable]
		public class LevelCompletionTarget
		{
			public int neededLevel;
		}

		public List<LevelCompletionTarget> LevelCompletionTargets = new List<LevelCompletionTarget>();
	}
}