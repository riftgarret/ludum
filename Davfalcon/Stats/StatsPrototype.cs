using System;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Abstract base class for <see cref="IStats"/>.
	/// </summary>
	[Serializable]
	public abstract class StatsPrototype : IStats
	{
		// Allow implementation to decide how stat is returned
		/// <summary>
		/// Gets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		public abstract int Get(Enum stat);

		/// <summary>
		/// Gets a stat by enum name.
		/// </summary>
		/// <param name="stat">The enum for the name of the stat.</param>
		public int this[Enum stat] => Get(stat);
	}
}
