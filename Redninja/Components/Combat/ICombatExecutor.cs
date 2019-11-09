﻿using System;
using Davfalcon;
using Redninja.Components.Buffs;
using Redninja.Components.Combat.Events;
using Redninja.Components.Skills;

namespace Redninja.Components.Combat
{
	public interface ICombatExecutor
	{
		event Action<IBattleEntity, Coordinate> EntityMoving;
		event Action<ICombatEvent> BattleEventOccurred;
		
		void MoveEntity(IBattleEntity entity, int newRow, int newCol);
		void MoveEntity(IBattleEntity entity, UnitPosition newPosition);
		void ApplyStatusEffect(IBattleEntity source, IBattleEntity target, IBuff effect);
		void RemoveStatusEffect(IBattleEntity entity, IBuff effect);		
		void DealDamage(IBattleEntity attacker, IBattleEntity defender, IStats skillStats, DamageType damageType);
		void DealTickDamage(IBattleEntity attacker, IBattleEntity defender, IStats skillStats, DamageType damageType);
	}
}
