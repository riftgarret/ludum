using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja.Components.Skills
{
	public interface ISkillProvider
	{
		IWeaponAttack GetAttack(IEnumerable<IWeapon> weapons);
		IEnumerable<ISkill> GetSkills();
	}
}
