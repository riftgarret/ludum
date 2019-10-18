using System;

namespace Davfalcon.Equipment
{
	public interface IEquipmentSlot<TEquipmentType, TEquipment>
		where TEquipmentType : Enum
	{
		TEquipmentType Type { get; }

		bool IsFull { get; }

		TEquipment Get();
	}
}
