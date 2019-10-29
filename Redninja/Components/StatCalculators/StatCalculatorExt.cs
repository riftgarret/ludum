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
		public static bool HasFlag(this IStats stats, Enum flag) => stats[flag] != 0;

		public static float AsScalor(this IStats stats, Enum flag) => stats[flag].AsScalor();

		public static float AsScalor(this int scale) => (scale + 100f) / 100f;
	}
}
