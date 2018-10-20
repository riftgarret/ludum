using System.Collections.Generic;
using Redninja.Components.Clock;

namespace Redninja
{
	public interface IBattleModel
	{
		IEnumerable<IUnitModel> Entities { get; }
		IClock Clock { get; }
	}
}
