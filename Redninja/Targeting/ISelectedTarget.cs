using System.Collections.Generic;

namespace Redninja.Targeting
{
	public interface ISelectedTarget
	{
		IEnumerable<IBattleEntity> GetValidTargets(IBattleEntityManager entityManager);
	}
}
