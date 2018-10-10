using System.Collections.Generic;
using System.Linq;

namespace Redninja.Targeting
{
	public class SelectedTargetPattern : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public ITargetPattern Pattern { get; }
		public Coordinate Anchor { get; }

		public SelectedTargetPattern(ITargetingRule rule, ITargetPattern pattern, int anchorRow, int anchorColumn)
			: this(rule, pattern, new Coordinate(anchorRow, anchorColumn))
		{ }

		public SelectedTargetPattern(ITargetingRule rule, ITargetPattern pattern, Coordinate anchor)
		{
			Rule = rule;
			Pattern = pattern;
			Anchor = anchor;
		}

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntityManager entityManager)
			=> entityManager.GetEntitiesInPattern(Anchor, Pattern).Where(e => Rule.IsValidTarget(e));
	}
}
