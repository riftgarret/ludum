using Redninja.Decisions;

namespace Redninja.AI
{
	public interface IAIRule
	{
		int RefreshTime { get; }
		string RuleName { get; }
		int Weight { get; }

		IBattleAction GenerateAction(IBattleEntity source, IBattleEntityManager bemskillMeta);
		bool IsValidTriggerConditions(IBattleEntity source, IBattleEntityManager entityManager);
	}
}