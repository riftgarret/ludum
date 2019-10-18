namespace Davfalcon.Stats
{
	/// <summary>
	/// Specifies a formula to use to calculate stats
	/// </summary>
	public interface IStatsCalculator
	{
		/// <summary>
		/// Perform a calculation on the given parameters.
		/// </summary>
		/// <param name="a">Value to be added.</param>
		/// <param name="b">Value to be added.</param>
		/// <param name="m">Multiplication factor.</param>
		int Calculate(int a, int b, int m);
	}
}
