using System;

namespace Redninja
{
	public interface IClock
	{
		float Time { get; }
		event Action<float> Tick;
	}
}
