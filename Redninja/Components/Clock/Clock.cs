using System;

namespace Redninja.Components.Clock
{
	internal class Clock : IClock
	{
		public float Time { get; private set; }
		public event Action<float> Tick;
		public void IncrementTime(float timeDelta)
		{
			Time += timeDelta;
			Tick?.Invoke(timeDelta);
		}
	}
}
