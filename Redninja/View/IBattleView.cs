﻿using Redninja.Components.Decisions;
using Redninja.Components.Combat.Events;

namespace Redninja.View
{
	public interface IBattleView
	{
		void OnBattleEventOccurred(ICombatEvent battleEvent);
	}
}
