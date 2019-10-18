using System;
using System.Collections.Generic;

namespace Redninja.Components.Skills
{
	public class UnitSkillManager : IUnitSkillManager
	{		
		public IEnumerable<ISkill> GetSkills()
		{
			throw new NotImplementedException();
		}

		public void Initialize(IUnit unit)
		{
		}
	}
}
