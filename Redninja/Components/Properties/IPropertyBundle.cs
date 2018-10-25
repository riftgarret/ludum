using System.Collections.Generic;

namespace Redninja.Components.Properties
{
	internal interface IPropertyBundle
	{
		IEnumerable<IPassiveProperty> PassiveProperties { get; }
		IEnumerable<ITriggeredProperty> GetActiveProperties { get; }
	}
}
