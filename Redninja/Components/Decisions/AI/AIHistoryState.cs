using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	internal class AIHistoryState : IAIHistoryState
	{
		private Dictionary<IAIRule, float> skillTimeUsed = new Dictionary<IAIRule, float>();
		private IBattleModel battleModel;

		public AIHistoryState(IBattleModel battleModel)
		{
			this.battleModel = battleModel;
		}

		public void AddEntry(IAIRule rule, IBattleAction resolvedAction)
			=> skillTimeUsed[rule] = battleModel.Clock.Time;

		public bool IsRuleReady(IAIRule rule)
			=> !skillTimeUsed.ContainsKey(rule) || battleModel.Clock.Time - skillTimeUsed[rule] > rule.RefreshTime;
	}
}
