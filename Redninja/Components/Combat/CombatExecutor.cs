using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Stats;
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

		struct CalculatorBundle
		{
			public readonly DamageType damageType;
			public readonly StatCalculator [] calculators;

			public CalculatorBundle(DamageType damageType, params StatCalculator[] calculators)
			{
				this.damageType = damageType;
				this.calculators = calculators;
			}
		}

		private IDictionary<DamageType, CalculatorBundle> calculatorDict;		
		private IBattleContext context;

		public CombatExecutor(IBattleContext context)
		{
			this.context = context;
			calculatorDict = new Dictionary<DamageType, CalculatorBundle>()
			{
				{ DamageType.Slash, new CalculatorBundle(
					DamageType.Slash,
					Calculators.SLASH_DMG,
					Calculators.SLASH_REDUCTION,
					Calculators.SLASH_RES,
					Calculators.SLASH_PEN
					)
				},
				{ DamageType.Fire, new CalculatorBundle(
					DamageType.Fire,
					Calculators.FIRE_DMG,
					Calculators.FIRE_REDUCTION,
					Calculators.FIRE_RES,
					Calculators.FIRE_PEN
					)
				},
				{ DamageType.Bleed, new CalculatorBundle(
					DamageType.Bleed,
					Calculators.BLEED_TICK_DAMAGE,
					Calculators.BLEED_TICK_REDUCTION
					)
				}
			};
		}


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

		public DamageEvent DealTickDamage(IBattleEntity attacker, IBattleEntity defender, IStatSource skillSource, DamageType damageType)
		{
			var oc = CreateOperationContext(attacker, defender, skillSource);
			ExecuteCalculators(calculatorDict[damageType], oc);
			var result = oc.BuildTickResult(damageType);
			var e = new DamageEvent(attacker, defender, result);
			ApplyDamage(e);
			context.SendEvent(e);
			return e;
		}

		public DamageEvent DealSkillDamage(IBattleEntity attacker, IBattleEntity defender, IStatSource skillSource, DamageType damageType)
		{
			var oc = CreateOperationContext(attacker, defender, skillSource);
			ExecuteCalculators(calculatorDict[damageType], oc);
			var result = oc.BuildSkillResult(damageType);
			var e = new DamageEvent(attacker, defender, result);
			ApplyDamage(e);
			context.SendEvent(e);
			return e;
		}

		private void ApplyDamage(DamageEvent damageEvent)
		{
			damageEvent.Target.HP.Current -= damageEvent.Total;
			
			if(damageEvent.DamageSourceType == DamageSourceType.Skill)
			{
				// TODO check for revenge operation.
			}

			if (damageEvent.Target.HP.Current <= 0)
			{
				// TODO create death event.
			}
		}		

		private SkillOperationResult GetBleedResult(IBattleEntity attacker, 
			IBattleEntity defender, 
			IStats skillStats)
		{
			IStats combinedStats = attacker.Stats.Join(skillStats);

			EventHistorian historian = new EventHistorian();
			historian.AddPropery(SkillOperationResult.Property.DamageRaw, "temp", combinedStats[Stat.BleedDamageExtra]);
			return new SkillOperationResult(DamageType.Bleed, historian);
		}

		private OperationContext CreateOperationContext(IBattleEntity attacker, IBattleEntity defender, IStatSource skillSource)
		{
			return new OperationContext(attacker, defender, skillSource);
		}

		private void ExecuteCalculators(CalculatorBundle bundle, OperationContext oc) => bundle.calculators.ForEach(x => x.DamageOperationProcess(oc));
	}
}
