using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Redninja.Components.Skills;

namespace Redninja.Components.Combat
{
	public class SkillStatsSource : IStatSource
	{
		private readonly ISkill skill;

		public SkillStatsSource(ISkill skill, IStats stats)
		{
			this.skill = skill;
			this.Stats = stats;
		}

		public string Name => skill.Name;

		public IStats Stats { get; private set; }
	}
}
