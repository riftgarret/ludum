using System;
using System.Collections.Generic;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public class UnitSkillManager : IUnitSkillManager
	{
		public IWeaponAttack GetAttack(IEnumerable<IWeapon> weapons)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ISkill> GetSkills()
		{
			throw new NotImplementedException();
		}

		public void Initialize(IUnit unit)
		{
		}
	}
}
