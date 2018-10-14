using Redninja.Skills;

namespace Redninja.Decisions
{
	/// <summary>
	/// This helper will provide instances that represent the state of processing the
	/// requirement for finishing a turn by deciding a skill and targets.
	/// </summary>
	public interface IDecisionHelper
	{
		IBattleEntityManager BattleEntityManager { get; }

		IActionPhaseHelper GetAvailableSkills(IBattleEntity entity);
		ITargetPhaseHelper GetTargetingManager(IBattleEntity entity, ISkill skill);
	}
}
