using System;

namespace Davfalcon
{
	/// <summary>
	/// Defines default stat operations.
	/// </summary>
	[Serializable]
	public class StatsOperations : IStatsOperations
	{
		/// <summary>
		/// Defines the seed value for aggregating multipliers. Should be 0 for additive and 1 for multiplicative stacking.
		/// </summary>
		public int AggregateSeed { get; protected set; } = 0;

		/// <summary>
		/// Defines the default method for aggregating multipliers.
		/// </summary>
		/// <param name="a">Accumulator value.</param>
		/// <param name="b">Value to add to the accumulator.</param>
		/// <returns>The new accumulator value.</returns>
		public virtual int Aggregate(int a, int b)
			=> a + b;

		/// <summary>
		/// Defines the default scaling formula.
		/// </summary>
		/// <param name="a">Value to be scaled.</param>
		/// <param name="b">Scalar modifier.</param>
		/// <returns>The scaled value.</returns>
		public virtual int Scale(int a, int b)
			=> a.Scale(b);

		/// <summary>
		/// Defines the default method for applying an inverse scale.
		/// </summary>
		/// <param name="a">Value to be scaled.</param>
		/// <param name="b">Scalar modifier.</param>
		/// <returns>The inversely scaled value.</returns>
		public virtual int ScaleInverse(int a, int b)
			=> Scale(a, -b);

		/// <summary>
		/// Defines the default formula to be used for stat calculation.
		/// </summary>
		/// <param name="a">The base stat.</param>
		/// <param name="b">The addition modifier.</param>
		/// <param name="m">The multiplicative modifier.</param>
		/// <returns>The calculated result.</returns>
		public virtual int Calculate(int a, int b, int m)
			=> Scale(a + b, m);

		protected StatsOperations() { }

		/// <summary>
		/// A singleton instance of the default <see cref="StatsOperations"/> definition.
		/// </summary>
		public static IStatsOperations Default = new StatsOperations();
	}
}
