﻿using Redninja.Components.Actions;
using Redninja.Components.Clock;

namespace Redninja.Entities.Decisions.AI
{
	/// <summary>
	/// History state to be passed to IAIRuleSet in order to record and check each
	/// transaction.
	/// </summary>
	public interface IAIHistoryState : IClockSynchronized
	{
		void AddEntry(IAIRule rule, IBattleAction resolvedAction);
		bool IsRuleReady(IAIRule rule);
	}
}
