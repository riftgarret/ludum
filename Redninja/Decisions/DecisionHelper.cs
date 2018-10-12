using Redninja.Targeting;
using Redninja.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Actions;

namespace Redninja.Decisions
{
	/// <summary>
	/// Decision Manager for selecting a skill and a target to convert into an action.
	/// </summary>
	public static class DecisionHelper
	{
		public static SkillSelectionMeta GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public static ISkillTargetingManager GetTargetingManager(IBattleEntity entity, IBattleEntityManager entityManager, ICombatSkill combatSkill)
			=> new SkillTargetMeta(entity, combatSkill, entityManager);
	}
}
