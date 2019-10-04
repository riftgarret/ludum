using System;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Aggregates two sets of stats.
	/// </summary>
	public class StatsAggregator : StatsPrototype
	{
		private readonly IAggregator aggregator;

		private readonly IStats a;
		private readonly IStats b;

		/// <summary>
		/// Gets the aggregated value of the specified stat across the two sets of stats.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns></returns>
		public override int Get(Enum stat)
		{
			return aggregator.Aggregate(a[stat], b[stat]);
		}

		private StatsAggregator() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsAggregator"/> class.
		/// </summary>
		/// <param name="a">The first set of stats to aggregate.</param>
		/// <param name="b">The second set of stats to aggregate.</param>
		/// <param name="aggregator">The <see cref="IAggregator"/> interface that defines the aggregation function to use.</param>
		public StatsAggregator(IStats a, IStats b, IAggregator aggregator)
		{
			this.a = a;
			this.b = b;

			this.aggregator = aggregator;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsAggregator"/> class using the aggregation function specified by the default <see cref="StatsOperations"/> implementation.
		/// </summary>
		/// <param name="a">The first set of stats to aggregate.</param>
		/// <param name="b">The second set of stats to aggregate.</param>
		public StatsAggregator(IStats a, IStats b)
			: this(a, b, StatsOperations.Default)
		{ }
	}
}
