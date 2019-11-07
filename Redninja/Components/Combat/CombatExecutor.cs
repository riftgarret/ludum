using System;
using Davfalcon;
using Redninja.Components.Buffs;
using Redninja.Components.Combat.Events;
using Redninja.Components.Skills;
using Redninja.Components.StatCalculators;

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
			BattleEventOccurred?.Invoke(new BuffEvent(source, target, effect));
		}

		public void RemoveStatusEffect(IBattleEntity entity, IBuff effect)
		{
			//resolver.RemoveBuff(entity, effect);
		}
		
		public void DealDamage(IBattleEntity attacker, IBattleEntity defender, ISkillOperationParameters paramz)
		{
			DamageEvent e = new DamageEvent(attacker, defender);
			e.PutResult(DamageType.Physical, GetPhysicalResult(attacker, defender, paramz));

			// TODO? possibly damage resource if needed.

			defender.HP.Current -= e.Total;
		}

		private DamageResult GetPhysicalResult(IBattleEntity attacker, IBattleEntity defender, ISkillOperationParameters paramz)
		{
			IStats combinedStats = attacker.Stats.Join(paramz.Stats);

			// TODO combine attacker and skill into a single combiend node
			return DamageResult.Create(
				combinedStats.GetPhysicalDamageTotal(),
				defender.Stats.GetPhysicalReductionTotal(),
				combinedStats.GetPhysicalPenetrationTotal(),
				defender.Stats.GetPhysicalResistanceTotal()
				);
		}
	}
}
