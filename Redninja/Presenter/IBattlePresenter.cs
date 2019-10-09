using System;
using Redninja.View;

namespace Redninja.Presenter
{
	public interface IBattlePresenter : IDisposable
	{
		void IncrementGameClock(float timeDelta);
		void Initialize(IBattleView view);
		void Start();
	}
}
