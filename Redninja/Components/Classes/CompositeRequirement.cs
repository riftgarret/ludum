using System;
using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Utils;

namespace Redninja.Components.Classes
{
	public class CompositeRequirement : ICharacteristicRequirement
	{
		public IEnumerable<ICharacteristicRequirement> Requirements { get; }

		public CompositeRequirement(IEnumerable<ICharacteristicRequirement> requirements)
		{
			this.Requirements = requirements;
		}

		public bool IsAvailable(IUnit unit)
		{
			return Requirements.AreAll(true, req => req.IsAvailable(unit));
		}
	}
}
