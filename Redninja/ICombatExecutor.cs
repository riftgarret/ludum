using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;

namespace Redninja
{
	public interface ICombatExecutor
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void InitializeEntity(IBattleEntity entity);
		void MoveEntity(IBattleEntity entity, int newRow, int newCol);
		void MoveEntity(IBattleEntity entity, EntityPosition newPosition);
		IDamageNode GetRawDamage(IBattleEntity attacker, IDamageSource source);
		IDefenseNode GetDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source);
		IDefenseNode GetDamage(IBattleEntity defender, IDamageNode incomingDamage);
		IDefenseNode DealDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source);
	}
}
