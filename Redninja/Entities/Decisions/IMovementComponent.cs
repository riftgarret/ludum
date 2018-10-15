﻿using Redninja.Components.Actions;
using Redninja.View;

namespace Redninja.Entities.Decisions
{
	public interface IMovementComponent : IMovementView
	{
		void AddPoint(Coordinate point);
		bool Back();
		IBattleAction GetAction();
	}
}
