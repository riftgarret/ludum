using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Presenter
{
	public interface IPresenterConfiguration
	{
		void AddPlayerCharacter(IUnit character, int team, Coordinate position, ISkillProvider skillProvider);		
		void AddAICharacter(IUnit character, int team, Coordinate position, AIBehavior behavior, string nameOverride = null);		
		void SetTeamGrid(int team, Coordinate gridSize);
	}
}
