using System;

namespace Redninja.Presenter
{
	public interface IBattlePresenter : IDisposable
	{
		void Configure(Action<IPresenterConfiguration> configFunc);
		void IncrementGameClock(float timeDelta);
		void Initialize();
		void Start();
	}
}
