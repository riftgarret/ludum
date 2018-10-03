using System;
using System.Collections.Generic;
using Redninja.BattleSystem.Targeting;

namespace Redninja.BattleSystem
{
	public interface IBattleEntityManager : IClock
	{
		IEnumerable<IBattleEntity> AllEntities { get; }
		IEnumerable<IBattleEntity> EnemyEntities { get; }
		IEnumerable<IBattleEntity> PlayerEntities { get; }

		event Action<IBattleEntity> DecisionRequired;

		void AddBattleEntity(IBattleEntity entity);
		IEnumerable<IBattleEntity> GetPattern(int anchorRow, int anchorColumn, bool isEnemies, ITargetPattern pattern);
		IEnumerable<IBattleEntity> GetRow(int anchorRow, bool isEnemy);
		void SetAction(IBattleEntity entity, IBattleAction action);
	}
}