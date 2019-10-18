using Redninja.Components.Combat;

namespace Redninja.Components.Equipment
{
	public interface IWeapon : IEquipment, IDamageSource
	{
		WeaponType WeaponType { get; }
	}
}
