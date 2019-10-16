using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	internal class StaticTarget : ITargetResolver
	{
		public IBattleEntity Target { get; }

		public StaticTarget(IBattleEntity target) => Target = target;

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntity user, IBattleModel battleModel)
			=> new List<IBattleEntity>(1) { Target }.AsReadOnly();
	}
}
