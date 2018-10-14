using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja.Skills
{
	public interface ISkillProvider
	{
		IWeaponAttack GetAttack(string className, IEnumerable<IWeapon> weapons);
		IEnumerable<ISkill> GetSkills(string className, int level);
	}
}
