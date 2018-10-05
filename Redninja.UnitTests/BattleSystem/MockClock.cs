using Redninja.BattleSystem;
using System;

namespace Redninja.UnitTests.BattleSystem
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
