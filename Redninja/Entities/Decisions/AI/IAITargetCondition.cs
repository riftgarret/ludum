﻿namespace Redninja.Entities.Decisions.AI
{
	public interface IAITargetCondition
	{
		bool IsValid(IBattleEntity entity);
	}
}