using System;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	public interface IUnitBuffManager //: IUnitBuffManager<IUnit, IBuff>, IDisposable
	{
		// need to define effect payload
		event Action<IBuff, IBattleEntity> Effect;

		event Action<IBuff> BuffExpired;

		void AddActiveBuff(IBuff buff);
	}
}
