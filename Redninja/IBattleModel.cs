using System.Collections.Generic;
using Redninja.Components.Targeting;

namespace Redninja
{
	public interface IBattleModel
	{
		IEnumerable<IEntityModel> Entities { get; }

		IEnumerable<IEntityModel> GetEntitiesInPattern(int anchorRow, int anchorColumn, ITargetPattern pattern);
		IEnumerable<IEntityModel> GetEntitiesInPattern(Coordinate anchor, ITargetPattern pattern);
		IEnumerable<IEntityModel> GetEntitiesInRow(int anchorRow);
	}
}
