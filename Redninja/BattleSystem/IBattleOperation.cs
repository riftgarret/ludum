using System;
using Davfalcon.Revelator.Combat;

namespace Redninja.BattleSystem
{
	public interface IBattleOperation
	{
		float ExecutionStartTime { get; }
		event Action<IBattleEvent> BattleEventOccurred;
		void Execute(IBattleEntityManager manager, ICombatResolver combatResolver);
	}
}
