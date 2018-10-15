using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Entities;

namespace Redninja.View
{
	public interface IMovementState
	{
		IBattleEntity Entity { get; }
		ActionTime Time { get; }
		IEnumerable<Coordinate> CurrentPath { get; }

		IEnumerable<Coordinate> GetAvailableTiles();
	}
}
