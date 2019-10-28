using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon;
using Davfalcon.Collections.Adapters;
using Davfalcon.Serialization;
using Davfalcon.Stats;

namespace Redninja.Components.Skills
{
	public class SkillOperationParameters : ISkillOperationParameters
	{
		public StatsMap EditableStats { get; } = new StatsMap();
		public IStats Stats => EditableStats;

		public ISet<Enum> SkillFlags { get; } = new HashSet<Enum>();
	}
}
