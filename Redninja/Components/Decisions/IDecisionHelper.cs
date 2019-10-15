using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// This helper will provide instances that represent the state of processing the
	/// requirement for finishing a turn by deciding a skill and targets.
	/// </summary>
	internal interface IDecisionHelper
	{
		IBattleModel BattleModel { get; }

		IMovementContext GetMovementContext(IBattleEntity entity);
		IActionContext GetActionsContext(IBattleEntity entity);
		ITargetingContext GetTargetingContext(IBattleEntity entity, ISkill skill);		
	}
}
