using System;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Exposes stat properties.
	/// </summary>
	public interface IStatsProperties : IStats
	{
		/// <summary>
		/// Gets the base stats.
		/// </summary>
		IStats Base { get; }

		int GetModificationBase(Enum stat);

		/// <summary>
		/// Returns an object containing detailed stat information.
		/// </summary>
		/// <param name="stat">The enum identifier for the stat.</param>
		/// <returns>An <see cref="IStatNode"/> containing detailed stat information.</returns>
		IStatNode GetStatNode(Enum stat);
	}
}
