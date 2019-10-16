using Davfalcon.Equipment;

namespace Redninja.Components.Equipment
{
	public interface IUnitEquipmentManager : IUnitEquipmentManager<IUnit, EquipmentType, IEquipment>
	{
		// unlikely that this will require additional props, just a vanity interface for now
	}
}
