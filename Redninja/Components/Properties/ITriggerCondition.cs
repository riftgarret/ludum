﻿using Redninja.Components.Combat.Events;

namespace Redninja.Components.Properties
{
	public interface ITriggerCondition
	{
		void IsValid(ICombatEvent battleEvent, IUnitModel source, IBattleModel battleModel);
	}
}
