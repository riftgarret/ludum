using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Actions;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class WeaponAttack : ISkill
	{
		private readonly List<IWeapon> weapons = new List<IWeapon>();

		public string Name => "Basic attack";
		public ActionTime Time { get; private set; }
		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public int BaseDamage => weapons[0].BaseDamage;
		public int CritMultiplier => weapons[0].CritMultiplier;
		public Enum BonusDamageStat => weapons[0].BonusDamageStat;
		public IEnumerable<Enum> DamageTypes => weapons[0].DamageTypes;

		public IBattleAction GetAction(IBattleEntity entity, ISelectedTarget[] targets)
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

		public class Builder : BuilderBase<WeaponAttack, ISkill, Builder>
		{
			public Builder()
				=> Reset();

			public override Builder Reset()
			{
				build = new WeaponAttack
				{
					// Need to add weapon range later
					Targets = new List<SkillTargetingSet>() { new SkillTargetingSet(new TargetingRule(TargetTeam.Enemy)) }.AsReadOnly()
				};
				return Builder;
			}

			public Builder SetActionTime(int prepare, int execute, int recover)
			{
				build.Time = new ActionTime(prepare, execute, recover);
				return this;
			}

			public Builder AddWeapon(IWeapon weapon)
			{
				build.weapons.Add(weapon);
				return Builder;
			}
		}
	}
}
