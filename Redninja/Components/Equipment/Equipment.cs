using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Equipment;

namespace Redninja.Components.Equipment
{
	[Serializable]
	public class Equipment : Equipment<IUnit, EquipmentType>, IEquipment
	{
		public override string Name { get; set; }

		public override IStats Stats { get; set; }		
	}
}
