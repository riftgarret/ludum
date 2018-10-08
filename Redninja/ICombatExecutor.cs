using System;
using Davfalcon.Nodes;
using Davfalcon.Revelator;

namespace Redninja
{
	public interface ICombatExecutor
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void InitializeEntity(IBattleEntity entity);
		void MoveEntity(IBattleEntity entity, int newRow, int newCol);
		void MoveEntity(IBattleEntity entity, EntityPosition newPosition);
		INode GetRawDamage(IBattleEntity attacker, IDamageSource source);
		INode GetDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source);
		INode GetDamage(INode incomingDamage, IBattleEntity defender, IDamageSource source);
	}
}
