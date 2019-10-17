using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Buffs;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Properties;

namespace Redninja
{
	public interface IBattleEntity : IUnit, IDisposable
	{
		int Team { get; set; }
		UnitPosition Position { get; }

		IUnitActionManager Actions { get; }
		IUnitBuffManager Buffs { get; }

		IEnumerable<ITriggeredProperty> TriggeredProperties { get; }

		void InitializeBattlePhase();
		void MovePosition(int row, int col);

		// not sure where this should go, depends on whether AI behavior can be changed
		void SetAIBehavior(AIRuleSet ruleSet);
	}
}
