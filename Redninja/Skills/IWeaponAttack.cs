using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja.Skills
{
	public interface IWeaponAttack : ISkill
	{
		IEnumerable<IWeapon> Weapons { get; }
	}
}
