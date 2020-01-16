using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Stats
{
	public abstract class StatsProviderTemplate : IStatsProvider
	{
		protected StatsProvider statsProvider = new StatsProvider();

		public IStats Stats => statsProvider.Stats;

		public IEnumerable<IStatSource> GetSources(Enum stat) => statsProvider.GetSources(stat);
	}
}
