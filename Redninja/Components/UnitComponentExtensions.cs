using Redninja.Components.Buffs;
using Redninja.Components.Equipment;

namespace Redninja.Components
{
	public static class UnitComponentExtensions
	{
		public static IUnitEquipmentManager GetEquipmentManager(this IUnit unit)
			=> unit.GetComponent<IUnitEquipmentManager>(UnitComponents.Equipment);

		//public static IUnitBuffManager GetBuffManager(this IUnit unit)
		//	=> unit.GetComponent<IUnitBuffManager>(UnitComponents.Buffs);
	}
}
