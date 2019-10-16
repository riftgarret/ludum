using System.Collections.Generic;
using Redninja.Components.Equipment;

namespace Redninja.Components.Skills
{
	public interface IWeaponAttack : ISkill
	{
		IEnumerable<IWeapon> Weapons { get; }
	}
}
