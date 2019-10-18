using System;
using System.Collections.Generic;
using Davfalcon.Equipment;

namespace Redninja.Components.Equipment
{
	[Serializable]
	public class Equipment : Equipment<IUnit, EquipmentType>, IEquipment, IUnit
	{
		protected override IUnit SelfAsUnit => this;

		protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
			=> StatFunctions.Resolve(baseValue, modifications);

		protected override Func<int, int, int> GetAggregator(Enum modificationType)
			=> StatFunctions.GetAggregator(modificationType);

		protected override int GetAggregatorSeed(Enum modificationType)
			=> StatFunctions.GetAggregatorSeed(modificationType);
	}
}
