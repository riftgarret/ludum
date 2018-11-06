using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Decisions;
using Redninja.Components.Properties;
using Redninja.Components.Combat;

namespace Redninja.Entities
{
	internal interface IBattleEntity : IUnitModel, IClockSynchronized
	{
		IBattleAction CurrentAction { get; }
		IActionDecider ActionDecider { get; }
		IEnumerable<ITriggeredProperty> TriggeredProperties { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
		void SetAction(IBattleAction action);
	}
}
