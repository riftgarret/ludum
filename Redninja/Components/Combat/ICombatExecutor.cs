using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{
	public interface ICombatExecutor
	{
		event Action<IUnitModel, Coordinate> EntityMoving;
		event Action<ICombatEvent> BattleEventOccurred;

		void InitializeEntity(IUnitModel entity);
		void CleanupEntity(IUnitModel entity);
		void MoveEntity(IUnitModel entity, int newRow, int newCol);
		void MoveEntity(IUnitModel entity, UnitPosition newPosition);
		void ApplyStatusEffect(IUnitModel source, IUnitModel target, IBuff effect);
		void RemoveStatusEffect(IUnitModel entity, IBuff effect);
		IDamageNode GetRawDamage(IUnitModel attacker, IDamageSource source);
		IDefenseNode GetDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source);
		IDefenseNode GetDamage(IUnitModel defender, IDamageNode incomingDamage);
		void DealDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source);
	}
}
