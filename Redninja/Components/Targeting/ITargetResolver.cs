using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	public interface ITargetResolver
	{
		IEnumerable<IUnitModel> GetValidTargets(IUnitModel user, IBattleModel battleModel);
	}
}
