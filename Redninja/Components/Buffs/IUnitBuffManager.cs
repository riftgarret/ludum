using System;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	public interface IUnitBuffManager : IUnitBuffManager<IUnit, IBuff>
	{
		// need to define effect payload
		event Action<IBuff, IUnit> Effect;

		event Action<IBuff> BuffExpired;

		// add method to hook up battle event handlers
	}
}
