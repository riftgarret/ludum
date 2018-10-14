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
	public class DecisionHelper : IDecisionHelper
	{
		public DecisionHelper(IBattleEntityManager manager)
		{
			BattleEntityManager = manager;
		}

		public IBattleEntityManager BattleEntityManager { get; }

		public IActionPhaseHelper GetAvailableSkills(IBattleEntity entity)
			=> new SkillSelectionMeta(entity, entity.Skills);

		public ITargetPhaseHelper GetTargetingManager(IBattleEntity entity, ICombatSkill combatSkill)
			=> new SkillTargetMeta(entity, combatSkill, BattleEntityManager);
	}
}
