using System.Collections.Generic;

namespace Redninja
{
	public interface IBattleModel
	{
		IEnumerable<IEntityModel> Entities { get; }
	}
}
