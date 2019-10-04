namespace Davfalcon
{
	/// <summary>
	/// Specify operations for aggregating values.
	/// </summary>
	public interface IAggregator
	{
		/// <summary>
		/// Gets the seed value for the aggregation function.
		/// </summary>
		int AggregateSeed { get; }

		/// <summary>
		/// Adds a value to an accumulator.
		/// </summary>
		/// <param name="a">The accumulated value.</param>
		/// <param name="b">The value be added to the accumulator.</param>
		/// <returns>The new accumulated value.</returns>
		int Aggregate(int a, int b);
	}
}
