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

		IMovementComponent GetMovementComponent(IEntityModel entity);
		ISkillsComponent GetAvailableSkills(IEntityModel entity);
		ITargetingComponent GetTargetingComponent(IEntityModel entity, ISkill skill);
	}
}
