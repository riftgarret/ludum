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

		public IMovementComponent GetMovementComponent(IUnitModel entity)
			=> new MovementComponent(entity, EntityManager);

		public ISkillsComponent GetAvailableSkills(IUnitModel entity)
			=> new SkillSelectionMeta(entity);

		public ITargetingComponent GetTargetingComponent(IUnitModel entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, EntityManager);
	}
}
