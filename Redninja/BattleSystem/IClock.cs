using System;

namespace Redninja.BattleSystem
{
	public interface IClock
	{
		float Time { get; }
		event Action<float> Tick;
	}
}
