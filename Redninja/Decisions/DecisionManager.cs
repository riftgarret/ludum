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

        public SkillSelectionMeta GetAvailableSkills(IBattleEntity entity) 
            => new SkillSelectionMeta(entity, entity.Skills);

        public SkillTargetMeta GetSelectableTargets(IBattleEntity entity, ICombatSkill combatSkill) 
            => new SkillTargetMeta(entity, combatSkill, entityManager);

        public IBattleAction CreateAction(IBattleEntity entity, ICombatSkill combatSkill, SelectedTarget target) 
            => new CombatSkillAction(entity, combatSkill, target);
    }
}
