using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	internal class AIRuleTracker : IAIRuleTracker
	{
		private Dictionary<IAIRule, float> skillTimeUsed = new Dictionary<IAIRule, float>();
		private IBattleContext context;

		public AIRuleTracker(IBattleContext context)
		{
			this.context = context;
		}

		public void AddEntry(IAIRule rule, IBattleAction resolvedAction)
			=> skillTimeUsed[rule] = context.BattleModel.Time;

		public bool IsRuleReady(IAIRule rule)
			=> !skillTimeUsed.ContainsKey(rule) || context.BattleModel.Time - skillTimeUsed[rule] > rule.RefreshTime;
	}
}
