using System;

namespace Redninja.BattleSystem
{
	public interface IClockSynchronized : IDisposable
	{
		void SetClock(IClock clock);
	}
}
