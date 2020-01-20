using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Stats
{
	public class StatsProvider : IStatsProvider
	{
		private ISet<IStatSource> sources = new HashSet<IStatSource>();	

		public IStats Stats { get; private set; }

		public void AddSource(IStatSource source) => sources.Add(source);

		public void RemoveSource(IStatSource source) => sources.Remove(source);

		private class StatsSourceProxy : StatsPrototype, IStats
		{
			private StatsProvider provider;

			public StatsSourceProxy(StatsProvider provider)
			{
				this.provider = provider;
			}

			public override IEnumerable<Enum> StatKeys => provider.sources.SelectMany(x => x.Stats.StatKeys).Distinct();

			public override int Get(Enum stat) => provider.sources.Sum(x => x.Stats.Get(stat));
		}

		public StatsProvider()
		{
			Stats = new StatsSourceProxy(this);
		}

		public IEnumerable<IStatSource> GetSources(Enum stat)
		{
			return sources.Where(x => x.Stats.StatKeys.Contains(stat));
		}

		public IEnumerable<IStatSource> AllSources() => sources;
	}
}
