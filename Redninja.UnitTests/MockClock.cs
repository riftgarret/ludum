using System;
using Redninja.Components.Clock;

namespace Redninja
{
	public class MockClock : IClock
    {
        public float Time { get; set; }
        public event Action<float> Tick;
        public void IncrementTime(float timeDelta)
        {
            Time += timeDelta;
            Tick?.Invoke(timeDelta);
        }
    }
}
