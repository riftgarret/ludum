using Redninja.Components.Actions;

namespace Redninja.Entities.Decisions.AI
{
	public interface IAIRule
	{
		int RefreshTime { get; }
		string RuleName { get; }
		int Weight { get; }

		IBattleAction GenerateAction(IEntityModel source, IDecisionHelper decisionHelper);
		bool IsValidTriggerConditions(IEntityModel source, IDecisionHelper decisionHelper);
	}
}
