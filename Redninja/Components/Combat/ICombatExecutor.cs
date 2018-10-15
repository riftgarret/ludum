using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Events;

namespace Redninja.Components.Combat
{
	public interface ICombatExecutor
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void InitializeEntity(IUnitModel entity);
		void MoveEntity(IUnitModel entity, int newRow, int newCol);
		void MoveEntity(IUnitModel entity, UnitPosition newPosition);
		IDamageNode GetRawDamage(IUnitModel attacker, IDamageSource source);
		IDefenseNode GetDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source);
		IDefenseNode GetDamage(IUnitModel defender, IDamageNode incomingDamage);
		IDefenseNode DealDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source);
	}
}
