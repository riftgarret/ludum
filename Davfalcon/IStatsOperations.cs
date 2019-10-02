namespace Davfalcon
{
	/// <summary>
	/// Specifies operations used to calculate stats.
	/// </summary>
	public interface IStatsOperations : IAggregator
	{
		/// <summary>
		/// Scales a value by another value. Used to define how multiplicative modifiers are applied.
		/// </summary>
		/// <param name="a">The base value.</param>
		/// <param name="b">The scale factor.</param>
		/// <returns>The scaled value.</returns>
		int Scale(int a, int b);

		/// <summary>
		/// Inversely scales a value by another value.
		/// </summary>
		/// <param name="a">The base value.</param>
		/// <param name="b">The scale factor.</param>
		/// <returns>The scaled value.</returns>
		int ScaleInverse(int a, int b);

		/// <summary>
		/// Perform a calculation on the given operands.
		/// </summary>
		/// <param name="a">Value to be added.</param>
		/// <param name="b">Value to be added.</param>
		/// <param name="m">Multiplier factor.</param>
		int Calculate(int a, int b, int m);
	}
}
