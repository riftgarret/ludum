using System;

namespace Davfalcon
{
	/// <summary>
	/// Extension methods for commonly performed math operations.
	/// </summary>
	public static class MathExtensions
	{
		/// <summary>
		/// Clamps a value to the specified bounds.
		/// </summary>
		/// <param name="value">The value to be clamped.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns><paramref name="min"/> if <paramref name="value"/> is less than <paramref name="min"/>; <paramref name="max"/> if <paramref name="value"/> is greater than <paramref name="max"/>; otherwise, <paramref name="value"/>.</returns>
		public static int Clamp(this int value, int min, int max)
			=> Math.Max(min, Math.Min(value, max));

		/// <summary>
		/// Scales a value by a factor.
		/// </summary>
		/// <param name="value">The value to be scaled.</param>
		/// <param name="factor">The factor to scale <paramref name="value"/> by. Treated as a percent increase when positive and as an inverse percent increase when negative.</param>
		/// <returns>The scaled value.</returns>
		public static int Scale(this int value, int factor)
		{
			return (int)(value * (factor > 0 ? 1 + factor / 100f : (factor < 0 ? 100f / (100 - factor) : 1)));
		}

		/// <summary>
		/// Takes a percent of a value then caps it to a maximum.
		/// </summary>
		/// <param name="value">The value to be capped.</param>
		/// <param name="cap">The flat cap.</param>
		/// <param name="percent">The percentage of <paramref name="value"/> to use.</param>
		/// <returns>The capped value.</returns>
		public static int Cap(this int value, int cap, int percent = 0)
		{
			int v = value;
			if (percent > 0) v = v * percent / 100;
			if (cap > 0) v = Math.Min(v, cap);
			return v;
		}
	}
}
