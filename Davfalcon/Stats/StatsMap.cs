using System;
using System.Collections.Generic;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Implements data structure for stat lookup.
	/// </summary>
	[Serializable]
	public class StatsMap : StatsPrototype, IStatsEditable
	{
		private readonly Dictionary<Enum, int> map = new Dictionary<Enum, int>();

		public override IEnumerable<Enum> StatKeys => map.Keys;

		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The enum identifier for the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		public override int Get(Enum stat) => map.ContainsKey(stat) ? map[stat] : 0;

		/// <summary>
		/// Sets a stat.
		/// </summary>
		/// <param name="stat">The enum identifier for the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IStatsEditable"/> instance. Used for chaining methods.</returns>
		public IStatsEditable Set(Enum stat, int value)
		{
			map[stat] = value;
			return this;
		}

		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The enum identifier for the stat.</param>
		new public int this[Enum stat]
		{
			get => base[stat];
			set => Set(stat, value);
		}
	}
}
