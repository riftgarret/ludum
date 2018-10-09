using Redninja.Skills;
using System.Collections.Generic;

namespace Redninja.Decisions
{
	public interface ISkillProvider
	{
		List<ICombatSkill> GetAvailableSkills(IBattleEntity entity);
	}
}
