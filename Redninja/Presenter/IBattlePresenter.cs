using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Actions;

namespace Redninja.Presenter
{
	public interface IBattlePresenter : IDisposable
	{
		void AddCharacter(IUnit character, int row, int col);
		void AddCharacter(IUnit character, IActionDecider actionDecider, int team, int row, int col);
		void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, int row, int col);
		void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, IActionDecider actionDecider, int team, int row, int col);
		void IncrementGameClock(float timeDelta);
		void Initialize();
		void Start();
	}
}
