using System.Collections.Generic;

namespace Redninja.Targeting
{
	public interface ITargetResolver
	{
		IEnumerable<IBattleEntity> GetValidTargets(IBattleEntity user, IBattleEntityManager entityManager);
	}
}
