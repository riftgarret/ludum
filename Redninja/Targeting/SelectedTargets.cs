using System.Collections.Generic;
using System.Linq;
using Davfalcon;

namespace Redninja.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTargets : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public IEnumerable<IBattleEntity> Targets { get; }

		public SelectedTargets(ITargetingRule rule, IEnumerable<IBattleEntity> targets)
		{
			Rule = rule;
			Targets = targets.ToNewReadOnlyCollectionSafe();
		}

		public SelectedTargets(ITargetingRule rule, IBattleEntity target)
			: this(rule, new List<IBattleEntity>() { target })
		{ }

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntityManager entityManager)
			=> Targets.Where(e => Rule.IsValidTarget(e));
	}
}
