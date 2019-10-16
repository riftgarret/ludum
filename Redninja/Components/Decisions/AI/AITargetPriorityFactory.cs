using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Decisions.AI
{
	public class AITargetPriorityFactory
	{
		private AITargetPriorityFactory() { }

		// maybe randomize this
		public static IAITargetPriority Any { get; } = As(entities => entities.First());

		private static IAITargetPriority As(SimplePriorityEvaluator.BestTarget bestTarget) => new SimplePriorityEvaluator(bestTarget);

		public class SimplePriorityEvaluator : IAITargetPriority
		{
			internal delegate IBattleEntity BestTarget(IEnumerable<IBattleEntity> validEntities);

			private BestTarget bestTargetDelegate;

			internal SimplePriorityEvaluator(BestTarget onBestTarget)
			{
				bestTargetDelegate = onBestTarget;
			}

			public IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities)
				=> bestTargetDelegate(validEntities);
		}
	}
}
