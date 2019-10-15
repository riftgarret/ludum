using Redninja.Components.Skills;

namespace Redninja.View
{
	public interface ISkillsCallbacks
	{
		void InitiateMovement(IBattleEntity entity);
		void SelectSkill(IBattleEntity entity, ISkill skill);
		void Wait(IBattleEntity entity);
		void Cancel();
	}
}
