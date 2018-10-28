using System;
using Redninja.Events;

namespace Redninja.Components.Conditions
{
	internal class EventCondition : ConditionBase
	{
		public EventCondition()
		{
		}

		bool IsEventConditionMet(IUnitModel self, IBattleEvent battleEvent, IBattleModel battleModel)
		{
			return false;
		}
	}
}
