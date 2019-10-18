﻿using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;

namespace Redninja.Components.Skills
{
	public class ConfigurableSkillProvider : ISkillProvider
	{
		public List<ISkill> Skills { get; } = new List<ISkill>();
		public ActionTime AttackTime { get; set; } = new ActionTime(1, 1, 1);		

		public IEnumerable<ISkill> GetSkills() => Skills;		
	}
}
