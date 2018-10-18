using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	public interface IMovementView
	{
		IUnitModel Entity { get; }
		ActionTime Time { get; }
		IEnumerable<Coordinate> CurrentPath { get; }

		IEnumerable<Coordinate> GetAvailableTiles();
	}
}
