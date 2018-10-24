using Davfalcon.Revelator.Borger;
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

		public static ISkill TargetedSkill { get; } = CombatSkill.Build("Targeted skill",
			args => args
				.SetDamage(20)
				.AddDamageType(DamageType.Physical),
			b => b
				.SetActionTime(5, 10, 2)
				.AddTargetingSet(TargetTeam.Enemy, TargetConditions.None, builder => builder
					.AddCombatRound(0.0f, OperationProviders.Damage)
					.AddCombatRound(0.2f, OperationProviders.Damage)
					.AddCombatRound(0.4f, OperationProviders.Damage)
					.AddCombatRound(0.8f, OperationProviders.Damage)));

		public static ISkill PatternSkill { get; } = CombatSkill.Build("Pattern skill",
			args => args
				.SetDamage(40)
				.AddDamageType(DamageType.Magical),
			b => b
				.SetActionTime(7, 5, 10)
				.AddTargetingSet(TargetPatternFactory.CreateRowPattern(), TargetTeam.Enemy, TargetConditions.None, builder => builder
					.AddCombatRound(0.25f, OperationProviders.Damage)
					.AddCombatRound(0.30f, OperationProviders.Damage)
					.AddCombatRound(0.5f, TargetPatternFactory.CreateRowPattern(1), OperationProviders.Damage)
					.AddCombatRound(0.55f, TargetPatternFactory.CreateRowPattern(1), OperationProviders.Damage)
					.AddCombatRound(0.75f, TargetPatternFactory.CreateRowPattern(2), OperationProviders.Damage)
					.AddCombatRound(0.8f, TargetPatternFactory.CreateRowPattern(2), OperationProviders.Damage)));
	}
}
