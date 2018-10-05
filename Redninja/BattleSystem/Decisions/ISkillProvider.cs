using Redninja.Core.Skills;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Decisions
{
    public interface ISkillProvider
    {
        List<ICombatSkill> GetAvailableSkills(IBattleEntity entity);
    }
}
