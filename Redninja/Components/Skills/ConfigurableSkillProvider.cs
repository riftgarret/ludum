using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public class ConfigurableSkillProvider : ISkillProvider
	{
		public List<ISkill> Skills { get; } = new List<ISkill>();
		public ActionTime AttackTime { get; set; } = new ActionTime(1, 1, 1);

		public IWeaponAttack GetAttack(IEnumerable<IWeapon> weapons)
			=> WeaponAttack.Build(b => b
					.SetActionTime(AttackTime)
					.AddWeapons(weapons));

		public IEnumerable<ISkill> GetSkills() => Skills;		
	}
}
