using System;

namespace Redninja.AI
{
	public class AIActionDecider : IActionDecider
	{
		private AIRuleSet ruleSet;
		private IAIHistoryState historyState;

		public bool IsPlayer => false;

		public AIActionDecider(AIRuleSet ruleSet, IAIHistoryState historyState)
		{
			this.ruleSet = ruleSet;
			this.historyState = historyState;
		}

		public event Action<IBattleEntity, IBattleAction> ActionSelected;

		public void ProcessNextAction(IBattleEntity source, IBattleEntityManager entityManager)
		{
			IBattleAction action = ruleSet.ResolveAction(source, entityManager, historyState);
			ActionSelected?.Invoke(source, action);
		}
	}
}
