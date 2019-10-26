using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	public static class PhysicalDamageCalculator
	{
		public static int GetPhysicalDamageTotal(this IStats stats)
		{
			int val = 0;
			val += stats[Stat.ATK];

			if (stats.HasFlag(Stat.FlagPhysicalDamageAlsoUsesCon))
				val += stats[Stat.CON];

			if (stats.HasFlag(Stat.WeaponTypeDagger))
				val += stats[Stat.PhysicalDaggerDamage];

			val *= stats[Stat.PhysicalDamageScale];

			return val;
		}

		public static int GetPhysicalReductionTotal(this IStats stats)
		{
			int maxReduction = 50; // maybe get this value somewhere else
			maxReduction += stats[Stat.PhysicalDamageReductionCap];

			int val = 0;
			val += stats[Stat.DEF];
			val += stats[Stat.PhysicalDamageReduction];

			val = Math.Min(maxReduction, val);

			return val;
		}

		public static int GetPhysicalResistanceTotal(this IStats stats)
		{
			return stats[Stat.PhysicalDamageResistance];
		}

		public static int GetPhysicalPenetrationTotal(this IStats stats)
		{
			return stats[Stat.PhysicalDamagePenetration];
		}		
	}
}
