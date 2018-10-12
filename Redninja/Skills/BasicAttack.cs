using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Davfalcon.Revelator;
using Redninja.Actions;

namespace Redninja.Skills
{
	public class BasicAttack : ICombatSkill, IDamageSource
	{
		private readonly IWeapon weapon;

		public string Name => "Basic attack";
		public ActionTime Time { get; private set; }
		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public int BaseDamage => weapon.BaseDamage;
		public int CritMultiplier => weapon.CritMultiplier;
		public Enum BonusDamageStat => weapon.BonusDamageStat;
		public IEnumerable<Enum> DamageTypes => weapon.DamageTypes;
	}
}
