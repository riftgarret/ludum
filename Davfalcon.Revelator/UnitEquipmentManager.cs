using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class UnitEquipmentManager : UnitModifier, IUnitEquipmentManager
	{
		private Davfalcon.IUnit last;
		protected override Davfalcon.IUnit InterfaceUnit => last;

		private ManagedEnumStringList equipmentSlots = new ManagedEnumStringList();
		private ManagedList<IEquipment> equippedSlots = new ManagedList<IEquipment>();

		public IEnumerable<Enum> EquipmentSlots { get => equipmentSlots.AsReadOnly(); }
		public IEnumerable<IEquipment> All { get => equippedSlots.AsReadOnly(); }

		private void BindEquipment()
		{
			Davfalcon.IUnit last = Target;
			for (int i = 0; i < equippedSlots.Count; i++)
			{
				if (equippedSlots[i] == null)
					continue;
				if (last != null)
					equippedSlots[i].Bind(last);
				last = equippedSlots[i];
			}
			this.last = last;
		}

		public override void Bind(Davfalcon.IUnit target)
		{
			base.Bind(target);
			BindEquipment();
		}

		private int GetSlotIndex(Enum slotType, int offset)
		{
			int index = -1;
			for (int n = 0; n <= offset; n++)
			{
				index = equipmentSlots.IndexOf(slotType, index + 1);
			}
			return index;
		}

		private bool IndexHasEquipment(int index)
			=> index > -1 && index < equippedSlots.Count && equippedSlots[index] != null;

		public void AddEquipmentSlot(Enum slotType)
		{
			equipmentSlots.Add(slotType);
			equippedSlots.Add(null);
		}

		public void RemoveEquipmentSlotIndex(int index)
		{
			equippedSlots.RemoveAt(index);
			equipmentSlots.RemoveAt(index);
			BindEquipment();
		}

		public IEquipment GetEquipment(Enum slot) => GetEquipment(slot, 0);
		public IEquipment GetEquipment(Enum slotType, int offset)
		{
			int index = GetSlotIndex(slotType, offset);

			return IndexHasEquipment(index) ? equippedSlots[index] : null;
		}

		public IEnumerable<IEquipment> GetAllEquipmentForSlot(Enum slot)
			=> equippedSlots.Where(equip => equip?.SlotType.Equals(slot) ?? false);

		public void Equip(IEquipment equipment) => Equip(equipment, 0);
		public void Equip(IEquipment equipment, int offset) => EquipSlotIndex(equipment, GetSlotIndex(equipment.SlotType, offset));
		public void EquipSlotIndex(IEquipment equipment, int index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException($"Index {index} does not exist.");

			if (!equipmentSlots[index].Equals(equipment.SlotType))
				throw new ArgumentException($"Equipment type ({equipment.SlotType}) does not match slot specified by index {index} ({equipmentSlots[index]}).");

			if (IndexHasEquipment(index))
				throw new InvalidOperationException("Something is already equipped at that index.");

			equippedSlots[index] = equipment;
			BindEquipment();
		}

		public void UnequipSlot(Enum slot) => UnequipSlot(slot, 0);
		public void UnequipSlot(Enum slotType, int offset) => UnequipSlotIndex(GetSlotIndex(slotType, offset));
		public void UnequipSlotIndex(int index)
		{
			if (!IndexHasEquipment(index))
				throw new InvalidOperationException("There is nothing equipped at that index.");

			equippedSlots[index] = null;
			BindEquipment();
		}
	}
}
