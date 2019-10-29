using System;
using System.Linq;

namespace Davfalcon.Stats
{
	public class CombinedStats : IStats
	{
		private IStats[] stats;

		public CombinedStats(params IStats[] stats) => this.stats = stats;

		public int this[Enum stat] => stats.Sum(x => x[stat]);

		public int Get(Enum stat) => stats.Sum(x => x.Get(stat));
	}
}
