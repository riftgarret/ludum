using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Decisions;
using Redninja.Data;

namespace Redninja.Presenter
{
	public interface IPresenterConfiguration
	{
		void LoadData(IDataLoader loader);
		void LoadJsonData(string configPath);
		void AddCharacter(IUnit character, int row, int col);
		void AddCharacter(IUnit character, IActionDecider actionDecider, int team, int row, int col);
		void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, int row, int col);
		void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, IActionDecider actionDecider, int team, int row, int col);
		void SetTeamGrid(int team, Coordinate gridSize);
	}
}
