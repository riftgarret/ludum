using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	internal class MovementComponent : IMovementComponent
	{
		private readonly IBattleModel battleModel;
		private readonly List<Coordinate> path = new List<Coordinate>();

		public IUnitModel Entity { get; }
		public ActionTime Time => GetActionTime();
		public IEnumerable<Coordinate> CurrentPath { get; }

		public MovementComponent(IUnitModel entity, IBattleModel battleModel)
		{
			this.battleModel = battleModel;
			Entity = entity;
			CurrentPath = path.AsReadOnly();
		}

		private ActionTime GetActionTime()
		{
			// Depends on movement conditions (how time scales with distance, etc)
			throw new NotImplementedException();
		}

		public IEnumerable<Coordinate> GetAvailableTiles()
		{
			// This method should tell the view which tiles to highlight
			throw new NotImplementedException();
		}

		public bool IsValidMovement(Coordinate point)
			=> !battleModel.Entities.Select(e => (Coordinate)e.Position).Contains(point);

		public void AddPoint(Coordinate point)
		{
			if (!IsValidMovement(point)) throw new InvalidOperationException("Given point is not a valid movement.");
			path.Add(point);
		}

		public bool Back()
		{
			if (path.Count > 0)
			{
				path.RemoveAt(path.Count - 1);
				return true; ;
			}
			return false;
		}

		public IBattleAction GetAction()
		{
			throw new NotImplementedException();
		}
	}
}
