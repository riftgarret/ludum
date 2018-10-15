using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Skills;

namespace Redninja.Entities
{
	public interface IBattleEntity : IEntityModel, IClockSynchronized
	{
		bool IsPlayerControlled { get; }
		IBattleAction CurrentAction { get; }
		ActionPhase Phase { get; }
		float PhasePercent { get; }
		IActionDecider ActionDecider { get; set; }
		IEnumerable<ISkill> Skills { get; }

		event Action<IBattleEntity> DecisionRequired;

		void InitializeBattlePhase();
		void SetAction(IBattleAction action);
	}
}
