using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon;
using Davfalcon.Stats;
using Redninja.Components.StatCalculators;

namespace Redninja
{
	public static class StatFunctions
	{
		public static int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
				=> modifications[StatModType.Additive] + baseValue.Scale(modifications[StatModType.Scaling]);

		public static Func<int, int, int> GetAggregator(Enum modificationType) => (a, b) => a + b;

		public static int GetAggregatorSeed(Enum modificationType) => 0;

		public static IStats Join(this IStats stats, params IStats[] moreStats)
			=> new StatsAggregator(moreStats.Append(stats).ToArray());

		// remove this
		public static int Calculate(this IStats stats, CalculatedStat calcStat)
		{
			switch (calcStat)
			{
				case CalculatedStat.HPTotal:
					return stats.CalculateTotalHp();					
				case CalculatedStat.ResourceTotal:
					return stats.CalculateTotalResource();
				case CalculatedStat.PhysicalDamageTotal:
					return stats.GetPhysicalDamageTotal();
				case CalculatedStat.PhysicalReductionTotal:
					return stats.GetPhysicalReductionTotal();
				case CalculatedStat.PhysicalResistanceTotal:
					return stats.GetPhysicalResistanceTotal();
				case CalculatedStat.PhysicalPenetrationTotal:
					return stats.GetPhysicalPenetrationTotal();
			}

			throw new InvalidOperationException("Illegal stat value unmapped: " + calcStat);
		}
	}
}
