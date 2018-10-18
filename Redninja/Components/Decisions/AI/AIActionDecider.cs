using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	public class AIActionDecider : IActionDecider
	{
		private readonly AIRuleSet ruleSet;
		private readonly IAIHistoryState historyState;
		private readonly IDecisionHelper decisionHelper;

		public AIActionDecider(AIRuleSet ruleSet, IAIHistoryState historyState, IDecisionHelper decisionHelper)
		{
			this.decisionHelper = decisionHelper;
			this.ruleSet = ruleSet;
			this.historyState = historyState;
		}

		public event Action<IUnitModel, IBattleAction> ActionSelected;

		public void ProcessNextAction(IUnitModel source, IBattleModel battleModel)
		{
			IBattleAction action = ruleSet.ResolveAction(source, decisionHelper, historyState);
			ActionSelected?.Invoke(source, action);
		}
	}
}
