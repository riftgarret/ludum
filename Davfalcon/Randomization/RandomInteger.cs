using System;

namespace Davfalcon.Randomization
{
	/// <summary>
	/// Generate random integers.
	/// </summary>
	public class RandomInteger : RandomBase
	{
		private readonly int min;
		private readonly int max;

		/// <summary>
		/// Gets a new random integer within the set range.
		/// </summary>
		/// <returns>A new random integer.</returns>
		public int Get()
		{
			return Generator.Next(min, max);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomInteger"/> class with a range from 0 to <see cref="Int32.MaxValue"/>.
		/// </summary>
		public RandomInteger() : this(0, Int32.MaxValue)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomInteger"/> class  with a range from 0 to the specified maximum value.
		/// </summary>
		/// <param name="max">The exclusive upper bound of the random integers that can be returned.</param>
		public RandomInteger(int max) : this(0, max)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomInteger"/> class  with a specified range.
		/// </summary>
		/// <param name="min">The inclusive lower bound of the random integers that can be returned.</param>
		/// <param name="max">The exclusive upper bound of the random integers that can be returned.</param>
		public RandomInteger(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
