using System;
using Davfalcon.Stats;

namespace Davfalcon.Equipment
{
	[Serializable]
	public abstract class Equipment<TUnit, TEquipmentType> : IEquipment<TUnit, TEquipmentType>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		public TEquipmentType EquipmentType { get; set; }

		public abstract string Name { get; set; }

		public abstract IStats Stats { get; set; }
	}
}
