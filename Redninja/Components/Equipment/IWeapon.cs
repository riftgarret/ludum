namespace Redninja.Components.Equipment
{
	public interface IWeapon : IEquipment
	{
		WeaponType WeaponType { get; }
	}
}
