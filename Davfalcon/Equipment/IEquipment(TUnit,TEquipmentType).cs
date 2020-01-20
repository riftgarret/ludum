using System;

namespace Davfalcon.Equipment
{
	public interface IEquipment<TUnit, TEquipmentType> : IStatSource
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		TEquipmentType EquipmentType { get; }
	}
}
