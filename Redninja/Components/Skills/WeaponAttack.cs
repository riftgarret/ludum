using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class WeaponAttack : ISkill, IWeaponAttack
	{
		private readonly List<IWeapon> weapons = new List<IWeapon>();

		public string Name => "Basic attack";
		public ActionTime Time { get; private set; }
		public IEnumerable<IWeapon> Weapons { get; private set; }
		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public int BaseDamage => weapons[0].BaseDamage;
		public int CritMultiplier => weapons[0].CritMultiplier;
		public Enum BonusDamageStat => weapons[0].BonusDamageStat;
		public IEnumerable<Enum> DamageTypes => weapons[0].DamageTypes;

		public IBattleAction GetAction(IUnitModel entity, ISelectedTarget[] targets)
		{
			int n = weapons.Count;
			float interval = 1f / n;
			float t = 0;
			ISkillResolver[] resolvers = new ISkillResolver[n];
			for (int i = 0; i < n; i++, t += interval)
			{
				// Maybe we can allow each weapon to be targeted separately?
				resolvers[i] = new WeaponAttackResolver(t, weapons[i], targets[0]);
			}
			return new SkillAction(entity, this, resolvers);
		}

		public static ISkill Build(Func<Builder, IBuilder<ISkill>> func)
			=> func(new Builder()).Build();

		public class Builder : BuilderBase<WeaponAttack, ISkill, Builder>
		{
			public Builder()
				=> Reset();

			public override Builder Reset()
			{
				WeaponAttack w = new WeaponAttack
				{
					// Need to add weapon range later
					Targets = new List<SkillTargetingSet>() { new SkillTargetingSet(new TargetingRule(TargetTeam.Enemy)) }.AsReadOnly(),
				};
				w.Weapons = w.weapons.AsReadOnly();
				return Reset(w);
			}

			public Builder SetActionTime(int prepare, int execute, int recover) => Self(w => w.Time = new ActionTime(prepare, execute, recover));

			public Builder AddWeapon(IWeapon weapon) => Self(w => w.weapons.Add(weapon));
		}
	}
}
