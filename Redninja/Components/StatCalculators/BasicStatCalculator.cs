using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	public static class BasicStatCalculator
	{
		public static int CalculateTotalHp(this IStats stats) => (int) (stats[Stat.HP] * stats.AsScalor(Stat.HPScale));

		public static int CalculateTotalResource(this IStats stats) => stats[Stat.Resource];
	}
}
