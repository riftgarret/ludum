using Redninja.Components.Skills;
using Redninja.System;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	internal class DecisionHelper : IDecisionHelper
	{
		public IBattleModel BattleModel { get; }
		public ISystemProvider Provider { get; }

		public DecisionHelper(IBattleModel battleModel, ISystemProvider provider)
		{
			BattleModel = battleModel;
			Provider = provider;
		}

		public IActionContext GetActionsContext(IUnitModel entity)
			=> new SkillSelectionContext(entity, Provider.GetSkillProvider(entity));

		public IMovementContext GetMovementContext(IUnitModel entity)
			=> new MovementContext(entity, BattleModel);

		public ITargetingContext GetTargetingContext(IUnitModel entity, ISkill skill)
			=> new SkillTargetingContext(entity, skill, BattleModel);
	}
}
