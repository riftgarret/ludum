using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// History state to be passed to IAIRuleSet in order to record and check each
	/// transaction.
	/// </summary>
	public interface IAIRuleTracker 
	{
		void AddEntry(IAIRule rule, IBattleAction resolvedAction);
		bool IsRuleReady(IAIRule rule);
	}
}
