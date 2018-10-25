using System;
using Davfalcon;

namespace Redninja.Components
{
	public interface IPassiveProperty : IItemProperty
	{
		IStats Additions { get; }
		IStats Multipliers { get; }
	}
}
