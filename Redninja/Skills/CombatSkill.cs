using System;
using System.Collections.Generic;
using Davfalcon.Collections.Adapters;
using Davfalcon.Revelator;
using Redninja.Actions;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class CombatSkill : ICombatSkill
	{
		public string Name { get; private set; }
		public ActionTime Time { get; private set; }

		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public int BaseDamage { get; private set; }
		public int CritMultiplier { get; private set; }
		public Enum BonusDamageStat { get; private set; }

		public ManagedEnumStringList DamageTypes { get; } = new ManagedEnumStringList();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		public class Builder : BuilderBase<CombatSkill, ICombatSkill>
		{
			private List<SkillTargetingSet> targets;

			public Builder()
			{
				Reset();
			}

			public Builder Reset()
			{
				targets = new List<SkillTargetingSet>();
				build = new CombatSkill()
				{
					Targets = targets.AsReadOnly()
				};
				return this;
			}

			public Builder SetName(string name)
			{
				build.Name = name;
				return this;
			}

			public Builder SetActionTime(int prepare, int execute, int recover)
			{
				build.Time = new ActionTime(prepare, execute, recover);
				return this;
			}

			public Builder SetDamage(int baseDamage, Enum bonusDamageStat = null)
			{
				build.BaseDamage = baseDamage;
				build.BonusDamageStat = bonusDamageStat;
				return this;
			}

			public Builder AddDamageType(Enum type)
			{
				build.DamageTypes.Add(type);
				return this;
			}

			public Builder AddTargetingSet(ITargetingRule targetingRule, Func<SkillTargetingSet.Builder, SkillTargetingSet.Builder> builderFunc)
			{
				targets.Add(builderFunc(new SkillTargetingSet.Builder(targetingRule)).Build());
				return this;
			}
		}
	}
}
