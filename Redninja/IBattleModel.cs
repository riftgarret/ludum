using System.Collections.Generic;
using Redninja.Components.Clock;

namespace Redninja
{
	public interface IBattleModel
	{
		float Time { get; }
		IEnumerable<IBattleEntity> Entities { get; }
		Coordinate GetGridSizeForTeam(int team);
	}
}
