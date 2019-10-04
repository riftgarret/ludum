using System;

namespace Davfalcon
{
	/// <summary>
	/// Exposes methods to access stats.
	/// </summary>
	public interface IStats
	{
		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		int Get(Enum stat);

		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		int this[Enum stat] { get; }
	}
}
