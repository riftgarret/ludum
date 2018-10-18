using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	internal class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleModel battleModel)
		{
			BattleModel = battleModel;
		}

		public IBattleModel BattleModel { get; }

		public IActionsContext GetActionsContext(IUnitModel entity)
			=> new SkillSelectionContext(entity);

		public IMovementContext GetMovementContext(IUnitModel entity)
			=> new MovementContext(entity, BattleModel);

		public ITargetingContext GetTargetingContext(IUnitModel entity, ISkill skill)
			=> new SkillTargetingContext(entity, skill, BattleModel);
	}
}
