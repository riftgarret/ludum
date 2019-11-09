using System;
using System.Collections.Generic;
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

		struct GenericDamageBundle
		{
			public DamageType damageType;
			public CalculatedStat damage, reduction, resistance, penetration;
		}

		private IDictionary<DamageType, GenericDamageBundle> genericTypeDict;
		private IBattleContext context;

		public CombatExecutor(IBattleContext context)
		{
			this.context = context;
			genericTypeDict = new Dictionary<DamageType, GenericDamageBundle>()
			{
				{ DamageType.Slash, new GenericDamageBundle() {
						damageType = DamageType.Slash,
						damage = CalculatedStat.SlashDamage,
						reduction = CalculatedStat.SlashReduction,
						resistance = CalculatedStat.SlashResistance,
						penetration = CalculatedStat.SlashPenetration
					}
				},
				{ DamageType.Fire, new GenericDamageBundle() {
					damageType = DamageType.Fire,
						damage = CalculatedStat.FireDamage,
						reduction = CalculatedStat.FireReduction,
						resistance = CalculatedStat.FireResistance,
						penetration = CalculatedStat.FirePenetration
					}
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

		public void DealTickDamage(IBattleEntity attacker, IBattleEntity defender, IStats skillStats, DamageType damageType)
			=> DealDamage(attacker, defender, skillStats, damageType, DamageSourceType.Tick);

		public void DealDamage(IBattleEntity attacker, IBattleEntity defender, IStats skillStats, DamageType damageType)
			=> DealDamage(attacker, defender, skillStats, damageType, DamageSourceType.Skill);

		private void DealDamage(IBattleEntity attacker, 
			IBattleEntity defender, 
			IStats skillStats, 
			DamageType damageType,
			DamageSourceType damageSourceType)
		{
			DamageEvent e = new DamageEvent(attacker, defender);
			e.DamageSourceType = damageSourceType;

			if (genericTypeDict.ContainsKey(damageType))
			{
				e.PutResult(damageType, GetGenericResult(attacker, defender, skillStats, genericTypeDict[damageType]));
			}
			else if (damageType == DamageType.Bleed)
			{
				e.PutResult(damageType, GetBleedResult(attacker, defender, skillStats));
			}
			else
			{
				throw new NotImplementedException($"Missing implementation for damageType: ");
			}

			defender.HP.Current -= e.Total;

			context.SendEvent(e);
		}

		private DamageResult GetBleedResult(IBattleEntity attacker, 
			IBattleEntity defender, 
			IStats skillStats)
		{
			IStats combinedStats = attacker.Stats.Join(skillStats);
			return DamageResult.Create(DamageType.Bleed, combinedStats[Stat.BleedDamageExtra], 0, 0, 0);
		}

		private DamageResult GetGenericResult(IBattleEntity attacker, 
			IBattleEntity defender, 
			IStats skillStats, 
			GenericDamageBundle bundle)
		{
			IStats combinedStats = attacker.Stats.Join(skillStats);

			// TODO combine attacker and skill into a single combiend node
			return DamageResult.Create(
				bundle.damageType,
				combinedStats.Calculate(bundle.damage),
				defender.Stats.Calculate(bundle.reduction),
				combinedStats.Calculate(bundle.penetration),
				defender.Stats.Calculate(bundle.resistance)
				);
		}
	}
}
