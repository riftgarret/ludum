using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	internal class StaticTarget : ITargetResolver
	{
		public IUnitModel Target { get; }

		public StaticTarget(IUnitModel target) => Target = target;

		public IEnumerable<IUnitModel> GetValidTargets(IUnitModel user, IBattleModel battleModel)
			=> new List<IUnitModel>(1) { Target }.AsReadOnly();
	}
}
