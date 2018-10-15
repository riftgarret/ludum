using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	public interface ITargetResolver
	{
		IEnumerable<IBattleEntity> GetValidTargets(IBattleEntity user, IBattleEntityManager entityManager);
	}
}
