using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{
	public interface ICombatExecutor
	{
		event Action<IBattleEntity, Coordinate> EntityMoving;
		event Action<ICombatEvent> BattleEventOccurred;

		void InitializeEntity(IBattleEntity entity);
		void CleanupEntity(IBattleEntity entity);
		void MoveEntity(IBattleEntity entity, int newRow, int newCol);
		void MoveEntity(IBattleEntity entity, UnitPosition newPosition);
		void ApplyStatusEffect(IBattleEntity source, IBattleEntity target, IBuff effect);
		void RemoveStatusEffect(IBattleEntity entity, IBuff effect);
		IDamageNode GetRawDamage(IBattleEntity attacker, IDamageSource source);
		IDefenseNode GetDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source);
		IDefenseNode GetDamage(IBattleEntity defender, IDamageNode incomingDamage);
		void DealDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source);
	}
}
