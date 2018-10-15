using System.Collections.Generic;
using System.Linq;

namespace Redninja.Entities.Decisions.AI
{
	public class AITargetPriorityFactory
	{
		private AITargetPriorityFactory() { }

		// maybe randomize this
		public static IAITargetPriority NoPriority => As(entities => entities.First());



		private static IAITargetPriority As(SimplePriorityEvaluator.BestTarget bestTarget) => new SimplePriorityEvaluator(bestTarget);

		public class SimplePriorityEvaluator : IAITargetPriority
		{
			internal delegate IEntityModel BestTarget(IEnumerable<IEntityModel> validEntities);

			private BestTarget bestTargetDelegate;

			internal SimplePriorityEvaluator(BestTarget onBestTarget)
			{
				bestTargetDelegate = onBestTarget;
			}

			public IEntityModel GetBestTarget(IEnumerable<IEntityModel> validEntities)
				=> bestTargetDelegate(validEntities);
		}
	}
}
