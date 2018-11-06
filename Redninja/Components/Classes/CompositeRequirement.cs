using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator;

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
			return Requirements.All(req => req.IsAvailable(unit));
		}
	}
}
