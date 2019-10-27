using System;
using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public class AIStatPriority : IAITargetPriority
	{
		public IStatEvaluator StatEvaluator { get; }

		public AITargetingPriorityQualifier Qualifier { get; }

		public AIPriorityType PriorityType { get; } = AIPriorityType.StatValue;

		public AIStatPriority(IStatEvaluator statEval,
			AITargetingPriorityQualifier qualifier)
		{
			StatEvaluator = statEval;
			Qualifier = qualifier;			
		}

		public IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities)
			=> AIHelper.FindBestMatch(validEntities, Qualifier, ex => StatEvaluator.Eval(ex));

		public override bool Equals(object obj)
			=> obj is AIStatPriority priority &&
				StatEvaluator == priority.StatEvaluator &&
				Qualifier == priority.Qualifier &&
				PriorityType == priority.PriorityType;

		public override int GetHashCode() => $"{StatEvaluator}{Qualifier}{PriorityType}".GetHashCode();
	}
}
