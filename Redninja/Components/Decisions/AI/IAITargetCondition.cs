﻿namespace Redninja.Components.Decisions.AI
{
	public interface IAITargetCondition
	{
		bool IsValid(IUnitModel entity);
	}
}