using System;

namespace Redninja
{
	public interface IBattleOperation
	{
		float ExecutionStartTime { get; }
		void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor);
	}
}
