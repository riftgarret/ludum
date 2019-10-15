using System;
using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Properties;

namespace Redninja
{
	public interface IBattleEntity : IUnit, IClockSynchronized
	{
		int Team { get; set; }
		UnitPosition Position { get; }

		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }
		IAIBehavior AIBehavior { get; }
		bool RequiresAction { get; }

		IBattleAction CurrentAction { get; }
		IActionContextProvider ActionContextProvider { get; }
		IEnumerable<ITriggeredProperty> TriggeredProperties { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
		void SetAction(IBattleAction action);
		void SetAIBehavior(AIRuleSet ruleSet);
	}
}
