using Davfalcon.Revelator.Borger;
using Redninja.Operations;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.ConsoleDriver.Objects
{
	public static class CombatSkills
	{
		public static ISkill BasicAttack { get; } = new WeaponAttack.Builder()
			.SetActionTime(2, 2, 2)
			.AddWeapon(Weapons.Sword)
			.Build();

		public static ISkill TwoHandedAttack { get; } = new WeaponAttack.Builder()
			.SetActionTime(3, 2, 3)
			.AddWeapon(Weapons.Shortsword)
			.AddWeapon(Weapons.Dagger)
			.Build();

		public static ISkill TargetedSkill { get; } = new CombatSkill.Builder()
			.SetName("Targeted skill")
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

		public static ISkill PatternSkill { get; } = new CombatSkill.Builder()
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
			)
			.Build();
	}
}
