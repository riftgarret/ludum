using System;

namespace Davfalcon.Equipment
{
	[Serializable]
	public abstract class Equipment<TUnit, TEquipmentType> : UnitStatsModifier<TUnit>, IEquipment<TUnit, TEquipmentType>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		public TEquipmentType EquipmentType { get; set; }
	}
}
