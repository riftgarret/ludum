using System.Collections.Generic;

namespace Redninja.Components.Properties
{
	public interface IPropertyBundle
	{
		IEnumerable<IPassiveProperty> StatProperties { get; }
		IEnumerable<ITriggeredProperty> TriggeredProperties { get; }
	}
}
