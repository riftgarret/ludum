using System;
using Davfalcon;
using Davfalcon.Equipment;

namespace Redninja.Components.Equipment
{
	[Serializable]
	public sealed class UnitEquipmentManager : UnitEquipmentManager<IUnit, EquipmentType, IEquipment>, IUnitEquipmentManager, IUnitComponent<IUnit>
	{
	}

	public static class UnitExtension
	{
		public static IUnitEquipmentManager GetEquipmentManager(this IUnit unit)
			=> unit.GetComponent<IUnitEquipmentManager>(UnitComponents.Equipment);
	}
}
