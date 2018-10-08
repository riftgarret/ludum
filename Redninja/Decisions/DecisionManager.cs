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
    public class DecisionManager
    {
        private IBattleEntityManager entityManager;

        public DecisionManager(IBattleEntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public AvailableSkillsResult GetAvailableSkills(IBattleEntity entity)
        {            
            
            return null;
        }

        public AvailableTargetsResult GetSelectableTargets(IBattleEntity entity, ICombatSkill combatSkill)
        {            
            // right now with a small grid, this could return 1 for each character, or 1 for each position
            // but i feel like there is a better way to communicate this.
            return null;
        }

        public IBattleAction CreateAction(IBattleEntity entity, ICombatSkill combatSkill, SelectedTarget target)
        {
            return new CombatSkillAction(entity, combatSkill, target);
        }
    }
}
