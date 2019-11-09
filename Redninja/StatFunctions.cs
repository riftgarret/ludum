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
				case CalculatedStat.HP:
					return stats.FinalHp();
				case CalculatedStat.Mana:
					return stats.FinalMana();
				case CalculatedStat.Stamina:
					return stats.FinalStamina();
				case CalculatedStat.SlashDamage:
					return stats.FinalSlashDamage();
				case CalculatedStat.SlashResistance:
					return stats.FinalSlashResistance();
				case CalculatedStat.SlashReduction:
					return stats.FinalSlashReduction();
				case CalculatedStat.SlashPenetration:
					return stats.FinalSlashPenetration();
				case CalculatedStat.FireDamage:
					return stats.FinalFireDamage();
				case CalculatedStat.FireResistance:
					return stats.FinalFireResistance();
				case CalculatedStat.FireReduction:
					return stats.FinalFireReduction();
				case CalculatedStat.FirePenetration:
					return stats.FinalFirePenetration();				
				case CalculatedStat.Level:
					return stats[Stat.Level];
				case CalculatedStat.Zero:
					return 0; 
			}

			throw new InvalidOperationException("Illegal stat value unmapped: " + calcStat);
		}
	}
}
