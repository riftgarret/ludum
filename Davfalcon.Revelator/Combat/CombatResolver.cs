using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Davfalcon.Randomization;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.Combat
{
	public class CombatResolver : ICombatResolver
	{
		#region Config
		private class Config
		{
			public ICombatOperations Operations { get; set; }
			public CombatStatBinding StatBindings { get; set; }
		}

		private class CombatStatBinding
		{
			public Enum Hit { get; set; }
			public Enum Dodge { get; set; }
			public Enum Crit { get; set; }
			public IList<Enum> VolatileStats { get; } = new List<Enum>();
			public IDictionary<Enum, Enum> DamageScalingMap { get; } = new Dictionary<Enum, Enum>();
			public IDictionary<Enum, Enum> DamageResistMap { get; } = new Dictionary<Enum, Enum>();
			public Enum DefaultDamageResource { get; set; }
			public IDictionary<Enum, IList<Enum>> DamageResourceMap { get; } = new Dictionary<Enum, IList<Enum>>();

			public Enum GetDamageScalingStat(Enum damageType)
				=> DamageScalingMap.ContainsKey(damageType) ? DamageScalingMap[damageType] : null;

			public Enum GetDamageResistStat(Enum damageType)
				=> DamageResistMap.ContainsKey(damageType) ? DamageResistMap[damageType] : null;

			public IEnumerable<Enum> ResolveDamageResource(params Enum[] damageTypes)
			{
				List<Enum> stats = new List<Enum>();
				foreach (Enum type in damageTypes)
				{
					if (DamageResourceMap.ContainsKey(type))
						stats.AddRange(DamageResourceMap[type]);
				}
				stats.Add(DefaultDamageResource);
				return stats;
			}

			public IEnumerable<Enum> ResolveDamageResource(IEnumerable<Enum> damageTypes)
				=> ResolveDamageResource(damageTypes.ToArray());
		}

		private Config config;

		public ICombatOperations Operations => config.Operations;
		#endregion

		#region Unit operations
		private void AdjustMaxVolatileStats(IUnit unit, IDictionary<Enum, int> prevValues)
		{
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.VolatileStats[stat] += unit.Stats[stat] - prevValues[stat];
			}
		}

		public void ApplyBuff(IUnit unit, IBuff buff, IUnit source = null)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}

			buff.Owner = source ?? unit;
			buff.Reset();
			unit.Buffs.Add(buff);

			AdjustMaxVolatileStats(unit, currentValues);
		}

		public void RemoveBuff(IUnit unit, IBuff buff)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}

			unit.Buffs.Remove(buff);

			AdjustMaxVolatileStats(unit, currentValues);
		}

		public int AdjustVolatileStat(IUnit unit, Enum stat, int change)
		{
			int initial = unit.VolatileStats[stat];
			unit.VolatileStats[stat] = (unit.VolatileStats[stat] + change).Clamp(0, unit.Stats[stat]);
			return unit.VolatileStats[stat] - initial;
		}

		public void Initialize(IUnit unit)
		{
			// Initialize volatile stats
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.VolatileStats[stat] = unit.Stats[stat];
			}

			// Apply buffs granted by equipment
			foreach (IEquipment equip in unit.Equipment.All)
			{
				foreach (IBuff buff in equip.GrantedBuffs)
				{
					ApplyBuff(unit, buff.DeepClone(), unit);
				}
			}
		}

		public void Upkeep(IUnit unit)
		{
			List<IBuff> expired = new List<IBuff>();

			foreach (IBuff buff in unit.Buffs)
			{
				// Apply repeating effects
				if (buff.Duration > 0 && buff.Remaining > 0 ||
					buff.Duration == 0)
					ApplyEffects(buff, buff.Owner, unit);

				// Tick buff timers
				buff.Tick();

				// Record expired buffs (cannot remove during enumeration)
				if (buff.Duration > 0 && buff.Remaining == 0)
					expired.Add(buff);
			}

			// Remove expired buffs
			foreach (IBuff buff in expired)
			{
				RemoveBuff(unit, buff);
			}
		}

		public void Cleanup(IUnit unit)
		{
			// Clean up volatile stats
			unit.VolatileStats.Clear();

			// Clear all buffs/debuffs
			unit.Buffs.Clear();
		}
		#endregion

		#region Combat operations
		public HitCheck CheckForHit(IUnit unit, IUnit target)
		{
			int hitChance = config.StatBindings.Hit != null ? unit.Stats[config.StatBindings.Hit] : 100;
			int dodgeChance = config.StatBindings.Dodge != null ? unit.Stats[config.StatBindings.Dodge] : 0;

			double threshold = config.Operations.CalculateHitChance(hitChance, dodgeChance).Clamp(0, 100) / 100f;
			bool hit = new CenterWeightedChecker(threshold).Check();

			if (config.StatBindings.Crit == null)
				return new HitCheck(threshold, hit);

			double critThreshold = MathExtensions.Clamp(unit.Stats[config.StatBindings.Crit], 0, 100) / 100f;
			bool crit = hit ? new SuccessChecker(critThreshold).Check() : false;

			return new HitCheck(threshold, hit, critThreshold, crit);
		}

		public IEnumerable<Enum> GetDamageScalingStats(IDamageSource source)
			=> GetDamageScalingStats(source.DamageTypes);

		public IEnumerable<Enum> GetDamageScalingStats(IEnumerable<Enum> damageTypes)
			=> damageTypes
				.Where(type => config.StatBindings?.GetDamageScalingStat(type) != null)
				.Select(type => config.StatBindings.GetDamageScalingStat(type));

		public IEnumerable<Enum> GetDamageDefendingStats(IDamageSource source)
			=> GetDamageDefendingStats(source.DamageTypes);

		public IEnumerable<Enum> GetDamageDefendingStats(IEnumerable<Enum> damageTypes)
			=> damageTypes
				.Where(type => config.StatBindings?.GetDamageResistStat(type) != null)
				.Select(type => config.StatBindings.GetDamageResistStat(type));

		public IDamageNode GetDamageNode(IUnit unit, IDamageSource source)
			=> new DamageNode(source, unit, this);

		public IDefenseNode GetDefenseNode(IUnit defender, IDamageNode damage)
			=> new DefenseNode(defender, damage, this);

		public IEnumerable<StatChange> ApplyDamage(IDefenseNode damage)
		{
			IUnit unit = damage.Defender;
			IEnumerable<Enum> types = damage.IncomingDamage.Source.DamageTypes;
			int value = damage.Value;

			// Get targeted resource points and apply damage pool in order
			List<StatChange> losses = new List<StatChange>();
			foreach (Enum stat in config.StatBindings.ResolveDamageResource(types))
			{
				// Apply up to the remaining number of points in the stat
				int actual = AdjustVolatileStat(unit, stat, -value);

				// Log the loss
				losses.Add(new StatChange(unit, stat, actual));

				// Subtract from remaining damage pool
				value += actual;

				// Break if all damage is applied
				if (value <= 0)
					break;
			}
			return losses;
		}

		public void ApplyEffects(IEffectSource source, IUnit owner, IUnit target)
		{
			foreach (IEffect effect in source.Effects)
			{
				effect.Resolve(owner, target, this);
			}
		}
		#endregion

		#region Nodes
		#endregion

		#region Builder
		private CombatResolver(Config config)
			=> this.config = config;

		public class Builder : IBuilder<ICombatResolver>
		{
			private Config config;
			private CombatStatBinding statBindings;

			public Builder() => Reset();

			public Builder Reset()
			{
				statBindings = new CombatStatBinding();
				config = new Config
				{
					Operations = CombatOperations.Default,
					StatBindings = statBindings
				};
				return this;
			}

			public Builder DefineCombatOperations(ICombatOperations operations)
			{
				config.Operations = operations;
				return this;
			}

			public Builder SetHitStats(Enum hit, Enum dodge = null, Enum crit = null)
			{
				statBindings.Hit = hit;
				statBindings.Dodge = dodge;
				statBindings.Crit = crit;
				return this;
			}

			public Builder AddVolatileStat(Enum stat)
			{
				statBindings.VolatileStats.Add(stat);
				return this;
			}

			public Builder AddDamageScaling(Enum damageType, Enum stat)
			{
				statBindings.DamageScalingMap[damageType] = stat;
				return this;
			}

			public Builder AddDamageResist(Enum damageType, Enum stat)
			{
				statBindings.DamageResistMap[damageType] = stat;
				return this;
			}

			public Builder AddDamageResourceRule(Enum damageType, Enum resourceStat)
			{
				if (!statBindings.DamageResourceMap.ContainsKey(damageType))
					statBindings.DamageResourceMap[damageType] = new List<Enum>();
				statBindings.DamageResourceMap[damageType].Add(resourceStat);
				return this;
			}

			public Builder SetDefaultDamageResource(Enum resourceStat)
			{
				statBindings.DefaultDamageResource = resourceStat;
				return this;
			}

			public ICombatResolver Build() => new CombatResolver(config);
		}

		public static ICombatResolver Default { get; } = new Builder().Build();
		#endregion
	}
}
