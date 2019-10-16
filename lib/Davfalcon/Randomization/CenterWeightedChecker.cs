namespace Davfalcon.Randomization
{
	/// <summary>
	/// Check for success within a given threshold using a center weighted distribution.
	/// </summary>
	public class CenterWeightedChecker : SuccessChecker
	{
		/// <summary>
		/// Generates a new random value.
		/// </summary>
		/// <returns></returns>
		protected override double GenerateValue()
		{
			return (Generator.NextDouble() + Generator.NextDouble()) / 2;
		}

		/// <summary>
		/// Initializes a new <see cref="CenterWeightedChecker"/> with a specified threshold.
		/// </summary>
		/// <param name="threshold">The threshold to use.</param>
		public CenterWeightedChecker(double threshold) : base(threshold) { }
	}
}
