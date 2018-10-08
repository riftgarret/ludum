using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Actions
{
    public class CombatSkillAction : BattleActionBase
    {
        private ICombatSkill skill;
        private IBattleEntity entity;

        public CombatSkillAction(IBattleEntity entity, ICombatSkill skill, SelectedTarget target)
            : base(skill.Time)
        {
            this.skill = skill;
            this.entity = entity;        
        }

        protected override void ExecuteAction(float timeDelta, float time)
        {
            // TODO
        }
    }
}
