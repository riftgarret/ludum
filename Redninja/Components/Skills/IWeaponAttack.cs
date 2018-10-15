using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja.Components.Skills
{
	public interface IWeaponAttack : ISkill
	{
		IEnumerable<IWeapon> Weapons { get; }
	}
}
