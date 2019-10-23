using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	public class PhysicalDamageCalculator : IStatCalculator
	{
		public void AssignCalculations(Unit unit)
		{
			unit.StatDerivations[CalculatedStat.PhysicalDamage] = stats => GetPhysicalDamageTotal(unit, stats);
			unit.StatDerivations[CalculatedStat.PhysicalReduction] = stats => GetPhysicalReductionTotal(unit, stats);
			unit.StatDerivations[CalculatedStat.PhysicalResistance] = stats => GetPhysicalResistanceTotal(unit, stats);
			unit.StatDerivations[CalculatedStat.PhysicalPenetration] = stats => GetPhysicalPenetrationTotal(unit, stats);
		}

		private int GetPhysicalDamageTotal(IUnit unit, IStatsProperties stats)
		{
			int val = 0;
			val += stats[Stat.ATK];

			if (stats.HasFlag(Stat.FlagPhysicalDamageAlsoUsesCon))
				val += stats[Stat.CON];

			if (unit.HasWeaponTypeEquiped(WeaponType.Dagger))
				val += stats[Stat.PhysicalDaggerDamage];

			val *= stats[Stat.PhysicalDamageScale];

			return val;
		}

		private int GetPhysicalReductionTotal(IUnit unit, IStatsProperties stats)
		{
			int maxReduction = 50; // maybe get this value somewhere else
			maxReduction += stats[Stat.PhysicalDamageReductionCap];

			int val = 0;
			val += stats[Stat.DEF];
			val += stats[Stat.PhysicalDamageReduction];

			val = Math.Min(maxReduction, val);

			return val;
		}

		private int GetPhysicalResistanceTotal(IUnit unit, IStatsProperties stats)
		{
			return stats[Stat.PhysicalDamageResistance];
		}

		private int GetPhysicalPenetrationTotal(IUnit unit, IStatsProperties stats)
		{
			return stats[Stat.PhysicalDamagePenetration];
		}		
	}
}
