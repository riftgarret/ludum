using Redninja.Components.Combat.Events;

namespace Redninja.Components.Properties
{
	public interface ITriggerCondition
	{
		void IsValid(ICombatEvent battleEvent, IBattleEntity source, IBattleModel battleModel);
	}
}
