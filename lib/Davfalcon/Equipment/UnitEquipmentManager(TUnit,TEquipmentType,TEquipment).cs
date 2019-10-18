using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Equipment
{
	[Serializable]
	public class UnitEquipmentManager<TUnit, TEquipmentType, TEquipment> : Modifier<TUnit>,
		IUnitEquipmentManager<TUnit, TEquipmentType, TEquipment>,
		IUnitComponent<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
		where TEquipment : class, IEquipment<TUnit, TEquipmentType>
	{
		// use a modifier stack to hold a single equipment per slot
		// takes advantage of the modifier stack's passthrough behavior
		protected class EquipmentSlot : IEquipmentSlot<TEquipmentType, TEquipment>
		{
			public TEquipmentType Type { get; }
			public IModifierStack<TUnit> Modifiers { get; } = new ModifierStack<TUnit>();
			public bool IsFull => Modifiers.Count > 0;
			public TEquipment Get() => IsFull ? Modifiers[0] as TEquipment : null;

			public void Set(TEquipment equipment)
			{
				if (IsFull) Modifiers[0] = equipment;
				else Modifiers.Add(equipment);
			}

			public void Unset() => Modifiers.Clear();

			public EquipmentSlot(TEquipmentType type)
			{
				Type = type;
			}
		}

		private readonly IModifierStack<TUnit> stack = new ModifierStack<TUnit>();
		private readonly List<EquipmentSlot> slots = new List<EquipmentSlot>();

		public IEquipmentSlot<TEquipmentType, TEquipment>[] EquipmentSlots => slots.ToArray();

		public void AddEquipmentSlot(TEquipmentType type)
		{
			EquipmentSlot slot = new EquipmentSlot(type);
			slots.Add(slot);
			stack.Add(slot.Modifiers);
		}

		public TEquipment GetEquipmentOfType(TEquipmentType type) => GetEquipmentOfType(type, 0);

		public TEquipment GetEquipmentOfType(TEquipmentType type, int offset) => GetAllSlotsOfType(type)[offset].Get();

		public IEnumerable<TEquipment> GetAllEquipment() => slots.Where(slot => slot.IsFull).Select(slot => slot.Get());

		public IEnumerable<TEquipment> GetAllEquipmentOfType(TEquipmentType type) => GetAllSlotsOfType(type).Where(slot => slot.IsFull).Select(slot => slot.Get());

		public void Equip(TEquipment equipment) => Equip(equipment, 0);

		public void Equip(TEquipment equipment, int offset) => SetSlotEquipment(GetAllSlotsMatchingTypeOf(equipment)[offset], equipment);

		public void EquipToSlotAt(int index, TEquipment equipment) => SetSlotEquipment(slots[index], equipment);

		public void UnequipSlot(TEquipmentType slotType) => UnequipSlot(slotType, 0);

		public void UnequipSlot(TEquipmentType slotType, int offset) => UnsetSlot(GetAllSlotsOfType(slotType)[offset]);

		public void UnequipSlotAt(int index) => UnsetSlot(slots[index]);

		protected virtual void SetSlotEquipment(EquipmentSlot slot, TEquipment equipment)
		{
			if (!slot.Type.Equals(equipment.EquipmentType))
				throw new InvalidOperationException($"Equipment type {equipment.EquipmentType} does not match slot type {slot.Type}");

			slot.Set(equipment);
		}

		protected virtual void UnsetSlot(EquipmentSlot slot)
		{
			slot.Unset();
		}

		private IList<EquipmentSlot> GetAllSlotsOfType(TEquipmentType type) => slots.FindAll(slot => slot.Type.Equals(type));

		private IList<EquipmentSlot> GetAllSlotsMatchingTypeOf(TEquipment equipment) => GetAllSlotsOfType(equipment.EquipmentType);

		public virtual void Initialize(TUnit unit) => unit.Modifiers.Add(this);

		public override TUnit AsModified() => stack.LastOrDefault()?.AsModified() ?? Target;

		public override void Bind(TUnit target)
		{
			base.Bind(target);
			stack.Bind(Target);
		}
	}
}
