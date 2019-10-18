using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja.Components.Skills
{
	public interface ISkillProvider
	{		
		IEnumerable<ISkill> GetSkills();
	}
}
