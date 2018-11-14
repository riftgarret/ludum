using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.AI;
using Redninja.Data;

namespace Redninja.Presenter
{
	public interface IPresenterConfiguration
	{
		void AddPC(IUnit character, int team, Coordinate position);		
		void AddNPC(IUnit character, int team, Coordinate position, AIBehavior behavior);		
		void SetTeamGrid(int team, Coordinate gridSize);
	}
}
