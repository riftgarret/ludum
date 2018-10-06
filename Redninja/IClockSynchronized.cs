using System;

namespace Redninja
{
	public interface IClockSynchronized : IDisposable
	{
		void SetClock(IClock clock);
	}
}
