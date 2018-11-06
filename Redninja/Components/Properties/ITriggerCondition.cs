using Redninja.Components.Combat.Events;

namespace Redninja.Components.Properties
{
	public interface ITriggerCondition
	{
		void IsValid(IBattleEvent battleEvent, IUnitModel source, IBattleModel battleModel);
	}
}
