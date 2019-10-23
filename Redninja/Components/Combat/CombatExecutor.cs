using System;
using Davfalcon;
using Redninja.Components.Buffs;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{
	public class CombatExecutor : ICombatExecutor
	{
		
		// I want to remove this
		public event Action<IBattleEntity, Coordinate> EntityMoving;
		public event Action<ICombatEvent> BattleEventOccurred;
		
		

		
		public void MoveEntity(IBattleEntity entity, int newRow, int newCol)
		{
			UnitPosition originalPosition = entity.Position;
			EntityMoving.Invoke(entity, new Coordinate(newRow, newCol));
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IBattleEntity entity, UnitPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public void ApplyStatusEffect(IBattleEntity source, IBattleEntity target, IBuff effect)
		{
			//resolver.ApplyBuff(target, effect);
			BattleEventOccurred?.Invoke(new StatusEffectEvent(source, target, effect));
		}

		public void RemoveStatusEffect(IBattleEntity entity, IBuff effect)
		{
			//resolver.RemoveBuff(entity, effect);
		}
		
		public void DealDamage(IBattleEntity attacker, IBattleEntity defender, IStats skillStats)
		{
			DamageEvent e = new DamageEvent();
			e.PutResult(DamageType.Physical, GetPhysicalResult(attacker, defender, skillStats));

			// TODO
			//defender.VolatileStats[CalculatedStat.HP] -= e.Total;
		}

		private DamageResult GetPhysicalResult(IBattleEntity attacker, IBattleEntity defender, IStats skillStats) => 
			// TODO combine attacker and skill into a single combiend node
			DamageResult.Create(
				attacker.Stats[CalculatedStat.PhysicalDamage],
				defender.Stats[CalculatedStat.PhysicalReduction],
				attacker.Stats[CalculatedStat.PhysicalPenetration],
				defender.Stats[CalculatedStat.PhysicalResistance]
				);
	}
}
