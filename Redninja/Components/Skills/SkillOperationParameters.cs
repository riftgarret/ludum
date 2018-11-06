using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Davfalcon.Collections.Adapters;
using Davfalcon.Serialization;

namespace Redninja.Components.Skills
{
	public class SkillOperationParameters : ISkillOperationParameters
	{
		private readonly ManagedEnumStringList damageTypes = new ManagedEnumStringList();

		public string Name { get; private set; }
		public int BaseDamage { get; private set; }
		public int CritMultiplier { get; private set; } = 1;
		public Enum BonusDamageStat { get; private set; }
		public IEnumerable<Enum> DamageTypes => damageTypes.AsReadOnly();

		private SkillOperationParameters() { }

		public class Builder : BuilderBase<SkillOperationParameters, Builder>
		{
			private readonly string skillName;

			internal Builder(string skillName)
			{
				this.skillName = skillName;
				Reset();
			}

			public override Builder Reset() => Reset(new SkillOperationParameters() { Name = skillName });
			public Builder SetDamage(int baseDamage) => Self(d => d.BaseDamage = baseDamage);
			public Builder SetBonusDamageStat(Enum stat) => Self(d => d.BonusDamageStat = stat);
			public Builder AddDamageType(Enum type) => Self(d => d.damageTypes.Add(type));
			public Builder AddDamageTypes(IEnumerable<Enum> types) => Self(d => d.damageTypes.AddRange(types.Select(t => new EnumString(t))));
			public Builder SetCritMultiplier(int crit) => Self(d => d.CritMultiplier = crit);
		}
	}
}
