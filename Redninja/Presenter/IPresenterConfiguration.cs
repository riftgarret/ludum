using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Presenter
{
	public interface IPresenterConfiguration
	{
		void AddPC(IUnit character, int team, Coordinate position, ISkillProvider skillProvider);		
		void AddNPC(IUnit character, int team, Coordinate position, AIBehavior behavior, string nameOverride = null);		
		void SetTeamGrid(int team, Coordinate gridSize);
	}
}
