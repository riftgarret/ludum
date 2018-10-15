using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Adapters;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class CombatSkill : ISkill
	{
		public string Name { get; private set; }
		public ActionTime Time { get; private set; }

		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public int BaseDamage { get; private set; }
		public int CritMultiplier { get; private set; }
		public Enum BonusDamageStat { get; private set; }

		public ManagedEnumStringList DamageTypes { get; } = new ManagedEnumStringList();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		public IBattleAction GetAction(IEntityModel entity, ISelectedTarget[] targets)
		{
			List<ISkillResolver> resolvers = new List<ISkillResolver>();
			for (int i = 0; i < Targets.Count; i++)
			{
				resolvers.AddRange(Targets[i].GetSkillResolvers(targets[i]));
			}
			return new SkillAction(entity, this, resolvers);
		}

		public static ISkill Build(Func<Builder, IBuilder<ISkill>> func)
			=> func(new Builder()).Build();

		public class Builder : BuilderBase<CombatSkill, ISkill, Builder>
		{
			private List<SkillTargetingSet> targets;

			public Builder() => Reset();

			public override Builder Reset()
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
