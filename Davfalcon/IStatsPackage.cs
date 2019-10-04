using System;
using Davfalcon.Nodes;

namespace Davfalcon
{
	/// <summary>
	/// Shows different views of a unit's stats. Includes base stats, modifiers, and stats after modifiers are applied.
	/// </summary>
	public interface IStatsPackage
	{
		/// <summary>
		/// Gets the stats after modifiers are applied.
		/// </summary>
		IStats Final { get; }

		/// <summary>
		/// Gets the base stats.
		/// </summary>
		IStats Base { get; }

		/// <summary>
		/// Gets the total additive modifiers.
		/// </summary>
		IStats Additions { get; }

		/// <summary>
		/// Gets the total multiplicative modifiers.
		/// </summary>
		IStats Multipliers { get; }

		INode GetBaseStatNode(Enum stat);
		IAggregatorNode GetAdditionsNode(Enum stat);
		IAggregatorNode GetMultipliersNode(Enum stat);
		INode GetStatNode(Enum stat);
	}
}
