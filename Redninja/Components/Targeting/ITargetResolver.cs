using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	public interface ITargetResolver
	{
		IEnumerable<IEntityModel> GetValidTargets(IEntityModel user, IBattleModel battleModel);
	}
}
