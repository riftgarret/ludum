using System;

namespace Redninja.Components.Clock
{
	public interface IClock
	{
		float Time { get; }
		event Action<float> Tick;
	}
}
