using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// This helper will provide instances that represent the state of processing the
	/// requirement for finishing a turn by deciding a skill and targets.
	/// </summary>
	public interface IDecisionHelper
	{
		IBattleModel BattleModel { get; }

		IMovementComponent GetMovementComponent(IUnitModel entity);
		ISkillsComponent GetAvailableSkills(IUnitModel entity);
		ITargetingComponent GetTargetingComponent(IUnitModel entity, ISkill skill);
	}
}
