using System;
using System.Collections.Generic;

namespace Davfalcon.Equipment
{
	public interface IUnitEquipmentManager<TUnit, TEquipmentType, TEquipment> : IModifier<TUnit>, IUnitComponent<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
		where TEquipment : class, IEquipment<TUnit, TEquipmentType>
	{
		IEquipmentSlot<TEquipmentType, TEquipment>[] EquipmentSlots { get; }

		void AddEquipmentSlot(TEquipmentType type);
		TEquipment GetEquipmentOfType(TEquipmentType type);
		TEquipment GetEquipmentOfType(TEquipmentType type, int offset);
		IEnumerable<TEquipment> GetAllEquipment();
		IEnumerable<TEquipment> GetAllEquipmentOfType(TEquipmentType type);
		void Equip(TEquipment equipment);
		void Equip(TEquipment equipment, int offset);
		void EquipToSlotAt(int index, TEquipment equipment);
		void UnequipSlot(TEquipmentType type);
		void UnequipSlot(TEquipmentType type, int offset);
		void UnequipSlotAt(int index);
	}
}
