using Redninja.Components.Actions;

namespace Redninja.Entities.Decisions.AI
{
	public interface IAIRule
	{
		int RefreshTime { get; }
		string RuleName { get; }
		int Weight { get; }

		IBattleAction GenerateAction(IBattleEntity source, IDecisionHelper decisionHelper);
		bool IsValidTriggerConditions(IBattleEntity source, IDecisionHelper decisionHelper);
	}
}
