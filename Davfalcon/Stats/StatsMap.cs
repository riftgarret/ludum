using System;
using System.Collections.Generic;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Implements data structure for stat lookup.
	/// </summary>
	[Serializable]
	public class StatsMap : StatsPrototype, IEditableStats
	{
		private Dictionary<Enum, int> map = new Dictionary<Enum, int>();

		/// <summary>
		/// Gets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		public override int Get(Enum stat) => map.ContainsKey(stat) ? map[stat] : 0;

		/// <summary>
		/// Sets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		public int Set(Enum stat, int value)
		{
			int old = Get(stat);
			map[stat] = value;
			return old;
		}

		/// <summary>
		/// Gets or sets a stat.
		/// </summary>
		/// <param name="stat">The name of the stat.</param> 
		new public int this[Enum stat]
		{
			get => Get(stat);
			set => Set(stat, value);
		}
	}
}
