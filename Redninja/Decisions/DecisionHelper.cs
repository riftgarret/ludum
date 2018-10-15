using Redninja.Skills;

namespace Redninja.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	internal class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleEntityManager manager)
		{
			BattleEntityManager = manager;
		}

		public IBattleEntityManager BattleEntityManager { get; }

		public IMovementComponent GetMovementComponent(IBattleEntity entity)
			=> new MovementComponent(entity, BattleEntityManager);

		public IActionPhaseHelper GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public ITargetingComponent GetTargetingComponent(IBattleEntity entity, ISkill skill)
			=> new SkillTargetingComponent(entity, skill, BattleEntityManager);
	}
}
