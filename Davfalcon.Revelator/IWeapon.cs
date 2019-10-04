using System;

namespace Davfalcon.Revelator
{
	public interface IWeapon : IEquipment, IDamageSource, IEffectSource
	{
		Enum WeaponType { get; }
	}
}
