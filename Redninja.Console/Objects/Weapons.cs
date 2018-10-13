using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;

namespace Redninja.ConsoleDriver.Objects
{
	public static class Weapons
	{
		public static IWeapon Sword { get; } = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
			.SetName("Longsword")
			.SetDamage(20)
			.AddDamageType(DamageType.Physical)
			.Build();

		public static IWeapon Shortsword { get; } = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
			.SetName("Shortsword")
			.SetDamage(10)
			.AddDamageType(DamageType.Physical)
			.Build();

		public static IWeapon Dagger { get; } = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Dagger)
			.SetName("Dagger")
			.SetDamage(5)
			.AddDamageType(DamageType.Physical)
			.Build();
	}
}
