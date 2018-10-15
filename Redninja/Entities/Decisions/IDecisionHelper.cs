using Redninja.Components.Skills;

namespace Redninja.Entities.Decisions
{
	/// <summary>
	/// This helper will provide instances that represent the state of processing the
	/// requirement for finishing a turn by deciding a skill and targets.
	/// </summary>
	public interface IDecisionHelper
	{
		IBattleEntityManager EntityManager { get; }

		IMovementComponent GetMovementComponent(IBattleEntity entity);
		IActionPhaseHelper GetAvailableSkills(IBattleEntity entity);
		ITargetingComponent GetTargetingComponent(IBattleEntity entity, ISkill skill);
	}
}
