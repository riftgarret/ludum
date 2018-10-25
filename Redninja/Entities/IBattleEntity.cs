﻿using System;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Decisions;
using Redninja.Components.Operations;

namespace Redninja.Entities
{
	internal interface IBattleEntity : IUnitModel, IClockSynchronized
	{
		IBattleAction CurrentAction { get; }
		IActionDecider ActionDecider { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationGenerator> ActionSet;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
		void SetAction(IBattleAction action);
	}
}
