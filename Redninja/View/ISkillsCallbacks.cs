using Redninja.Components.Skills;

namespace Redninja.View
{
	public interface ISkillsCallbacks
	{
		void InitiateMovement(IUnitModel entity);
		void SelectSkill(IUnitModel entity, ISkill skill);
		void Wait(IUnitModel entity);
		void Cancel();
	}
}
