using System.Collections.Generic;

namespace Redninja.Targeting
{
	public interface ITarget
	{
		IEnumerable<IBattleEntity> GetValidTargets();
	}
}
