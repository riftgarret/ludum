using Redninja.Components.Skills;

namespace Redninja.Entities.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	internal class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleEntityManager entityManager)
		{
			EntityManager = entityManager;
		}

		public IBattleEntityManager EntityManager { get; }

		public IMovementComponent GetMovementComponent(IBattleEntity entity)
			=> new MovementComponent(entity, EntityManager);

		public IActionPhaseHelper GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public ITargetingComponent GetTargetingComponent(IBattleEntity entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, EntityManager);
	}
}
