using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	static class StatCalculatorExt 
	{
		/// <summary>
		/// Assign calculators to units.
		/// </summary>
		/// <param name="unit"></param>
		public static void BindCalculators(this Unit unit)
		{
			var calculators = new List<IStatCalculator>()
			{
				new PhysicalDamageCalculator()
			};

			calculators.ForEach(calc => calc.AssignCalculations(unit));
		}

		public static bool HasFlag(this IStatsProperties stats, Enum flag) => stats[flag] != 0;

		public static float AsScalor(this IStatsProperties stats, Enum flag) => ((float)stats[flag] + 100f) / 100f;

		// TODO figure out if has weapon type
		public static bool HasWeaponTypeEquiped(this IUnit unit, WeaponType weaponType) => true;
	}


}
