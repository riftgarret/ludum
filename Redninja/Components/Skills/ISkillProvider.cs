using System.Collections.Generic;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public interface ISkillProvider
	{
		IWeaponAttack GetAttack(IEnumerable<IWeapon> weapons);
		IEnumerable<ISkill> GetSkills();
	}
}
