using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Buffs;
using Redninja.Components.Combat;
using Redninja.Components.Decisions.AI;

namespace Redninja
{
	public interface IBattleEntity : IUnit, IDisposable
	{
		int Team { get; set; }
		UnitPosition Position {  get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		IUnitActionManager Actions { get; }
		IUnitBuffManager Buffs { get; }

		// dunno about these, are they necessary?
		LiveStatContainer HP { get; }
		LiveStatContainer Mana { get; }
		LiveStatContainer Stamina { get; }

		IReadOnlyDictionary<LiveStat, LiveStatContainer> LiveStats { get; }

		void InitializeBattlePhase();
		void MovePosition(int row, int col);

		// not sure where this should go, depends on whether AI behavior can be changed
		void SetAIBehavior(AIRuleSet ruleSet);
	}
}
