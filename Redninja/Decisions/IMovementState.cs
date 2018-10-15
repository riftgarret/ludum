using System.Collections.Generic;
using Redninja.Actions;

namespace Redninja.Decisions
{
	public interface IMovementState
	{
		IBattleEntity Entity { get; }
		ActionTime Time { get; }
		IEnumerable<Coordinate> CurrentPath { get; }

		IEnumerable<Coordinate> GetAvailableTiles();
	}
}
