using System;
using System.Collections.Generic;

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
		/// <param name="stat">The enum identifier for the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		int Get(Enum stat);

		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The enum identifier for the stat.</param>
		int this[Enum stat] { get; }

		/// <summary>
		/// List of known Stats.
		/// </summary>
		IEnumerable<Enum> StatKeys { get; }
	}
}
