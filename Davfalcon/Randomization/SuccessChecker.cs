using System;

namespace Davfalcon.Randomization
{
	/// <summary>
	/// Check for success within a given threshold. Random values under the threshold are considered a success.
	/// </summary>
	public class SuccessChecker : RandomBase, ISuccessCheck
	{
		private readonly double threshold;

		/// <summary>
		/// Generates a new random value.
		/// </summary>
		/// <returns>The new random value.</returns>
		protected virtual double GenerateValue()
		{
			return Generator.NextDouble();
		}

		/// <summary>
		/// Makes a new check.
		/// </summary>
		/// <returns><c>true</c> if the check was successful; otherwise, <c>false</c>.</returns>
		public bool Check()
		{
			return GenerateValue() <= threshold;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SuccessChecker"/> class with a specified threshold.
		/// </summary>
		/// <param name="threshold">The threshold to use.</param>
		public SuccessChecker(double threshold)
		{
			if (threshold < 0 || threshold > 1.0)
				throw new ArgumentOutOfRangeException("Success threshold must be greater than or equal to 0.0 and less than or equal to 1.0");
			this.threshold = threshold;
		}
	}
}
