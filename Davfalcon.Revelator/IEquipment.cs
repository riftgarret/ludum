using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEquipment : IItem, IStatsModifier
	{
		Enum SlotType { get; }
		IEnumerable<IBuff> GrantedBuffs { get; }
	}
}
