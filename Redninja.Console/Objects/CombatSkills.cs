using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon.Revelator.Borger;
using Redninja.Operations;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.ConsoleDriver.Objects
{
	public static class CombatSkills
	{
		public static ICombatSkill DemoTargetedSkill { get; } = new CombatSkill.Builder()
			.SetName("Demo skill")
			.SetActionTime(5, 10, 2)
			.SetDamage(40)
			.AddDamageType(DamageType.Physical)
			.AddTargetingSet(new TargetingRule(TargetTeam.Enemy), builder => builder
				.AddCombatRound(0.0f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.2f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.4f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.8f, (e, t, s) => new DamageOperation(e, t, s))
			)
			.Build();
	}
}
