using System.Collections.Generic;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public interface ISkillProvider
	{		
		IEnumerable<ISkill> GetSkills();
	}
}
