using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Events;

namespace Redninja
{
	public interface ICombatExecutor
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void InitializeEntity(IEntityModel entity);
		void MoveEntity(IEntityModel entity, int newRow, int newCol);
		void MoveEntity(IEntityModel entity, EntityPosition newPosition);
		IDamageNode GetRawDamage(IEntityModel attacker, IDamageSource source);
		IDefenseNode GetDamage(IEntityModel attacker, IEntityModel defender, IDamageSource source);
		IDefenseNode GetDamage(IEntityModel defender, IDamageNode incomingDamage);
		IDefenseNode DealDamage(IEntityModel attacker, IEntityModel defender, IDamageSource source);
	}
}
