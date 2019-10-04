using System;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Performs math across a set of stats.
	/// </summary>
	public class StatsCalculator : StatsPrototype
	{
		private readonly IStatsOperations resolver;

		private readonly IStats original;
		private readonly IStats additions;
		private readonly IStats multipliers;

		/// <summary>
		/// Gets the resulting stat after performing calculations.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The stat after calculations.</returns>
		public override int Get(Enum stat)
		{
			return resolver.Calculate(original[stat], additions[stat], multipliers[stat]);
		}

		private StatsCalculator() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsCalculator"/> class that will calculate using the specified stat operands and resolver.
		/// </summary>
		/// <param name="original">The original set of stats to use.</param>
		/// <param name="additions">A set of values to add to each stat.</param>
		/// <param name="multipliers">A set of values to multiply each stat.</param>
		/// <param name="resolver">An object that specifies the calculation formula to use. If null, the default formula will be used.</param>
		public StatsCalculator(IStats original, IStats additions, IStats multipliers, IStatsOperations resolver)
		{
			this.original = original;
			this.additions = additions;
			this.multipliers = multipliers;

			this.resolver = resolver ?? StatsOperations.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsCalculator"/> class that will calculate using the specified stat operands and default resolver.
		/// </summary>
		/// <param name="original">The original set of stats to use.</param>
		/// <param name="additions">A set of values to add to each stat.</param>
		/// <param name="multipliers">A set of values to multiply each stat.</param>
		public StatsCalculator(IStats original, IStats additions, IStats multipliers)
			: this(original, additions, multipliers, null)
		{ }
	}
}
