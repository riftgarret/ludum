using System;

namespace Redninja.Components.Clock
{
	public interface IClockSynchronized : IDisposable
	{
		void SetClock(IClock clock);
	}
}
