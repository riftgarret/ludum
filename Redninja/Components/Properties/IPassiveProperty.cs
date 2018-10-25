using Davfalcon;

namespace Redninja.Components.Properties
{
	internal interface IPassiveProperty : IItemProperty
	{
		IStats Additions { get; }
		IStats Multipliers { get; }
	}
}
