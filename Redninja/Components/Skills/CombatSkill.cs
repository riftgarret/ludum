using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Davfalcon.Collections.Adapters;
using Davfalcon.Revelator;
using Davfalcon.Serialization;
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

		public IBattleAction GetAction(IUnitModel entity, ISelectedTarget[] targets)
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
				CombatSkill skill = new CombatSkill()
				{
					Targets = targets.AsReadOnly()
				};
				return Reset(skill);
			}

			public Builder SetName(string name) => Self(s => s.Name = name);
			public Builder SetActionTime(int prepare, int execute, int recover) => Self(s => s.Time = new ActionTime(prepare, execute, recover));
			public Builder SetActionTime(ActionTime actionTime) => Self(s => s.Time = actionTime);
			public Builder SetDamage(int baseDamage) => Self(w => w.BaseDamage = baseDamage);
			public Builder SetBonusDamageStat(Enum stat) => Self(w => w.BonusDamageStat = stat);
			public Builder AddDamageType(Enum type) => Self(w => w.DamageTypes.Add(type));
			public Builder AddDamageTypes(IEnumerable<Enum> types) => Self(w => w.DamageTypes.AddRange(types.Select(t => new EnumString(t))));
			public Builder SetCritMultiplier(int crit) => Self(w => w.CritMultiplier = crit);
			public Builder AddTargetingSet(SkillTargetingSet skillTargetingSet) => Self(s => targets.Add(skillTargetingSet));
			public Builder AddTargetingSet(ITargetingRule targetingRule, Func<SkillTargetingSet.Builder, SkillTargetingSet.Builder> builderFunc)
				=> Self(s => targets.Add(builderFunc(new SkillTargetingSet.Builder(targetingRule)).Build()));
			public Builder AddTargetingSets(IEnumerable<SkillTargetingSet> skillTargetingSets) => Self(s => targets.AddRange(skillTargetingSets));
		}
	}
}
