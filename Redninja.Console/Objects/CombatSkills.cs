using Davfalcon.Revelator.Borger;
using Redninja.Components.Operations;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.ConsoleDriver.Objects
{
	public static class CombatSkills
	{
		public static ISkill BasicAttack { get; } = WeaponAttack.Build(b => b
			.SetActionTime(2, 2, 2)
			.AddWeapon(Weapons.Sword));

		public static ISkill TwoHandedAttack { get; } = WeaponAttack.Build(b => b
			.SetActionTime(3, 2, 3)
			.AddWeapon(Weapons.Shortsword)
			.AddWeapon(Weapons.Dagger));

		public static ISkill TargetedSkill { get; } = CombatSkill.Build(b => b
			.SetName("Targeted skill")
			.SetActionTime(5, 10, 2)
			.SetDamage(40)
			.AddDamageType(DamageType.Physical)
			.AddTargetingSet(new TargetingRule(TargetTeam.Enemy), builder => builder
				.AddCombatRound(0.0f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.2f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.4f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.8f, (e, t, s) => new DamageOperation(e, t, s))
			));

		public static ISkill PatternSkill { get; } = CombatSkill.Build(b => b
			.SetName("Pattern skill")
			.SetActionTime(7, 5, 10)
			.SetDamage(20)
			.AddDamageType(DamageType.Magical)
			.AddTargetingSet(new TargetingRule(TargetPatternFactory.CreateRowPattern(), TargetTeam.Enemy), builder => builder
				.AddCombatRound(0.25f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.30f, (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.5f, TargetPatternFactory.CreateRowPattern(1), (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.55f, TargetPatternFactory.CreateRowPattern(1), (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.75f, TargetPatternFactory.CreateRowPattern(2), (e, t, s) => new DamageOperation(e, t, s))
				.AddCombatRound(0.8f, TargetPatternFactory.CreateRowPattern(2), (e, t, s) => new DamageOperation(e, t, s))
			));
	}
}
