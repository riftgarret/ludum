using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public class ConfigurableSkillProvider : ISkillProvider
	{
		public List<ISkill> Skills { get; } = new List<ISkill>();
		public ActionTime AttackTime { get; set; } = new ActionTime(1, 1, 1);		

		public IEnumerable<ISkill> GetSkills() => Skills;		
	}
}
