using System;
using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja
{
	public interface IBattlePresenter : IDisposable
	{
		void AddCharacter(IUnit character, int row, int col);
		void AddCharacter(IUnit character, IActionDecider actionDecider, int team, int row, int col);
		void AddCharacter(IBuilder<IUnit> builder, int row, int col);
		void AddCharacter(IBuilder<IUnit> builder, IActionDecider actionDecider, int team, int row, int col);
		void IncrementGameClock(float timeDelta);
		void Initialize();
		void Start();
	}
}
