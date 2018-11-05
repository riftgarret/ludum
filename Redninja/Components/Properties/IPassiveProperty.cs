using Davfalcon;

namespace Redninja.Components.Properties
{
	public interface IPassiveProperty : IItemProperty
	{
		IStats Additions { get; }
		IStats Multipliers { get; }
	}
}
