using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Stats
{
	public class StatsAggregator : IStats
	{
		private IStats[] stats;

		public StatsAggregator(params IStats[] stats) => this.stats = stats;

		public int this[Enum stat] => stats.Sum(x => x[stat]);

		public IEnumerable<Enum> StatKeys => stats.SelectMany(x => x.StatKeys).Distinct();

		public int Get(Enum stat) => stats.Sum(x => x.Get(stat));
	}
}
