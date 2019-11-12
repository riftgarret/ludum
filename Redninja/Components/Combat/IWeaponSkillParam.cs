using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat
{
	public interface IWeaponSkillParam
	{
		DamageType DamageType { get; }
		WeaponSlotType SlotType { get; }
		WeaponType WeaponType { get; }
	}
}
