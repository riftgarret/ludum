using System;
using Davfalcon;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Entities;

namespace Redninja.Components.Actions
{
	public interface IUnitActionManager : IClockSynchronized, IUnitComponent<IUnit>
	{
		// copied from IUnitModel/IBattleEntity
		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }
		bool RequiresAction { get; }
		IBattleAction CurrentAction { get; }

		// not sure if these should still use IBattleEntity
		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		void SetAction(IBattleAction action);
	}
}
