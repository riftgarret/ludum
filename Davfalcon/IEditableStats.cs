using System;

namespace Davfalcon
{
	/// <summary>
	/// Exposes methods to access and edit stats.
	/// </summary>
	public interface IEditableStats : IStats
	{
		/// <summary>
		/// Gets or sets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		new int this[Enum stat] { get; set; }

		/// <summary>
		/// Sets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>The previous value of the stat.</returns>
		int Set(Enum stat, int value);
	}
}
