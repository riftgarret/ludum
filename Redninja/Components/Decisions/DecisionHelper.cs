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

		public IMovementComponent GetMovementComponent(IUnitModel entity)
			=> new MovementComponent(entity, BattleModel);

		public ISkillsComponent GetAvailableSkills(IUnitModel entity)
			=> new SkillSelectionMeta(entity);

		public ITargetingComponent GetTargetingComponent(IUnitModel entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, BattleModel);
	}
}
