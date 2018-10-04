﻿using System;

namespace Redninja.BattleSystem
{
	public interface IBattleOperation
	{
		float ExecutionStartTime { get; }
		event Action<IBattleEvent> BattleEventOccurred;
		void Execute(IBattleEntityManager manager, ICombatExecutor combatResolver);
	}
}