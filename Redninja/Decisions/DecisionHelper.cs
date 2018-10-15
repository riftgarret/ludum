using Redninja.Skills;

namespace Redninja.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	public class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleEntityManager manager)
		{
			BattleEntityManager = manager;
		}

		public IBattleEntityManager BattleEntityManager { get; }

		public IActionPhaseHelper GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public ITargetPhaseHelper GetTargetingManager(IBattleEntity entity, ISkill skill)
			=> new SkillTargetMeta(entity, skill, BattleEntityManager);
	}
}
