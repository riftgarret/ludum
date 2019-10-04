using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitEquipmentManager : IUnitModifier
	{
		IEnumerable<Enum> EquipmentSlots { get; }
		IEnumerable<IEquipment> All { get; }

		void AddEquipmentSlot(Enum slotType);
		void RemoveEquipmentSlotIndex(int index);
		IEquipment GetEquipment(Enum slot);
		IEquipment GetEquipment(Enum slotType, int offset);
		IEnumerable<IEquipment> GetAllEquipmentForSlot(Enum slot);
		void Equip(IEquipment equipment);
		void Equip(IEquipment equipment, int offset);
		void EquipSlotIndex(IEquipment equipment, int index);
		void UnequipSlot(Enum slot);
		void UnequipSlot(Enum slotType, int offset);
		void UnequipSlotIndex(int index);
	}
}
