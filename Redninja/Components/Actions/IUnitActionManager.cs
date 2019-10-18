using System;
using Davfalcon;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;

namespace Redninja.Components.Actions
{
	public interface IUnitActionManager : IUnitComponent<IUnit>, IDisposable
	{
		// copied from IUnitModel/IBattleEntity
		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }
		bool RequiresAction { get; }
		IBattleAction CurrentAction { get; }
		IActionContextProvider ActionContextProvider { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		void SetAction(IBattleAction action);
	}
}
