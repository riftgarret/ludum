using System;
using Davfalcon;
using Davfalcon.Equipment;

namespace Redninja.Components.Equipment
{
	[Serializable]
	public sealed class UnitEquipmentManager : UnitEquipmentManager<IUnit, EquipmentType, IEquipment>, IUnitEquipmentManager, IUnitComponent<IUnit>
	{
	}
}
