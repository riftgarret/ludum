using System.Collections.Generic;

namespace Redninja
{
	public interface IBattleModel
	{
		IEnumerable<IUnitModel> Entities { get; }
	}
}
