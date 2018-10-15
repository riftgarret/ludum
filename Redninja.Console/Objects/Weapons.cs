using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;

namespace Redninja.ConsoleDriver.Objects
{
	public static class Weapons
	{
		public static IWeapon Sword { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
			.SetName("Longsword")
			.SetDamage(20)
			.AddDamageType(DamageType.Physical));

		public static IWeapon Shortsword { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
			.SetName("Shortsword")
			.SetDamage(10)
			.AddDamageType(DamageType.Physical));

		public static IWeapon Dagger { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Dagger, b => b
			.SetName("Dagger")
			.SetDamage(5)
			.AddDamageType(DamageType.Physical));
	}
}
