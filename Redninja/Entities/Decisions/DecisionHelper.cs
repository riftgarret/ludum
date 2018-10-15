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

		public IMovementComponent GetMovementComponent(IEntityModel entity)
			=> new MovementComponent(entity, EntityManager);

		public ISkillsComponent GetAvailableSkills(IEntityModel entity)
			=> new SkillSelectionMeta(entity);

		public ITargetingComponent GetTargetingComponent(IEntityModel entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, EntityManager);
	}
}
