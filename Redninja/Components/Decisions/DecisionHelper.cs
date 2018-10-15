using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	internal class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleModel entityModel)
		{
			EntityModel = entityModel;
		}

		public IBattleModel EntityModel { get; }

		public IMovementComponent GetMovementComponent(IBattleEntity entity)
			=> new MovementComponent(entity, EntityModel);

		public IActionPhaseHelper GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public ITargetingComponent GetTargetingComponent(IBattleEntity entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, EntityModel);
	}
}
