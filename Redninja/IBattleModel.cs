using System.Collections.Generic;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleModel
	{
		IEnumerable<IBattleEntity> AllEntities { get; }
		IEnumerable<IBattleEntity> EnemyEntities { get; }
		IEnumerable<IBattleEntity> PlayerEntities { get; }

		IEnumerable<IBattleEntity> GetEntitiesInPattern(int anchorRow, int anchorColumn, ITargetPattern pattern);
		IEnumerable<IBattleEntity> GetEntitiesInPattern(Coordinate anchor, ITargetPattern pattern);
		IEnumerable<IBattleEntity> GetEntitiesInRow(int anchorRow);
	}
}
