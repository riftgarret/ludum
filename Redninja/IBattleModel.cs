using System.Collections.Generic;

namespace Redninja
{
	public interface IBattleModel
	{
		float Time { get; }
		IEnumerable<IUnitModel> Entities { get; }
		Coordinate GetGridSizeForTeam(int team);
	}
}
