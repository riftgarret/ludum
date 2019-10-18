using System;
using System.Collections.Generic;
using Davfalcon;

namespace Redninja
{
	public static class StatFunctions
	{
		public static int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
				=> modifications[StatModType.Additive] + baseValue.Scale(modifications[StatModType.Scaling]);

		public static Func<int, int, int> GetAggregator(Enum modificationType) => (a, b) => a + b;

		public static int GetAggregatorSeed(Enum modificationType) => 0;
	}
}
