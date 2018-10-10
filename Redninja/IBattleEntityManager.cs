using System;
using System.Collections.Generic;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleEntityManager
	{
		IEnumerable<IBattleEntity> AllEntities { get; }
		IEnumerable<IBattleEntity> EnemyEntities { get; }
		IEnumerable<IBattleEntity> PlayerEntities { get; }

		event Action<IBattleEntity> DecisionRequired;

		void AddBattleEntity(IBattleEntity entity, IClock clock);
		void RemoveBattleEntity(IBattleEntity entity);
		IEnumerable<IBattleEntity> GetEntitiesInPattern(int anchorRow, int anchorColumn, ITargetPattern pattern);
		IEnumerable<IBattleEntity> GetEntitiesInPattern(Coordinate anchor, ITargetPattern pattern);
		IEnumerable<IBattleEntity> GetEntitiesInRow(int anchorRow);
		void SetAction(IBattleEntity entity, IBattleAction action);
		void InitializeBattlePhase();
	}
}