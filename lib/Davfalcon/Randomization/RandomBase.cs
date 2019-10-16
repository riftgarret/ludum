using System;

namespace Davfalcon.Randomization
{
	/// <summary>
	/// Abstract base class for generating random values.
	/// </summary>
	public abstract class RandomBase
	{
		private static readonly Random random = new Random();

		/// <summary>
		/// Gets the singleton random number generator.
		/// </summary>
		protected Random Generator
		{
			get { return random; }
		}
	}
}
