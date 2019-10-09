using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	public interface IAIBehavior
	{
		AIRuleSet RuleSet { get; }
		IBattleAction DetermineAction();
		AIActionDecisionResult LastResult { get; }
	}
}
