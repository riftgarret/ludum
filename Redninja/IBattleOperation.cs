using System;

namespace Redninja
{
	public interface IBattleOperation
	{
		void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor);
	}
}
