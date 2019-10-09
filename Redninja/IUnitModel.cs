using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Properties;

namespace Redninja
{
	public interface IUnitModel : IUnit
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
	}
}
