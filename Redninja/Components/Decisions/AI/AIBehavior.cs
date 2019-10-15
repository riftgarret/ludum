using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	internal class AIBehavior : IAIBehavior
	{
		private AIExecutor aiExecutor;

		public AIRuleSet RuleSet { get; }

		public AIActionDecisionResult LastResult { get; private set; }

		public AIBehavior(IBattleContext context, IBattleEntity unit, AIRuleSet ruleSet)
		{
			RuleSet = ruleSet;
			this.aiExecutor = new AIExecutor(context, unit, ruleSet, new AIRuleTracker(context));			
		}		

		public IBattleAction DetermineAction()
		{
			LastResult = aiExecutor.ResolveAction();
			return LastResult.Result;
		}
		
	}
}
