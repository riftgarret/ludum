using System.Collections.Generic;

namespace Redninja.Components
{
	public interface IPropertyBundle
	{
		IEnumerable<IPassiveProperty> PassiveProperties { get; }
		IEnumerable<IActiveProperty> GetActiveProperties { get; }
	}
}
