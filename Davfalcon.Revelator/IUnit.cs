using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnit : Davfalcon.IUnit
	{
		IDictionary<Enum, int> VolatileStats { get; }
		IUnitEquipmentManager Equipment { get; }
		IUnitModifierStack Buffs { get; }
	}
}
