﻿using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Targeting
{
	internal class SelectedTargetPattern : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public ITargetPattern Pattern { get; }
		public int Team { get; }
		public Coordinate Anchor { get; }

		IBattleEntity ISelectedTarget.Target => null;

		public SelectedTargetPattern(ITargetingRule rule, ITargetPattern pattern, int team, int anchorRow, int anchorColumn)
			: this(rule, pattern, team, new Coordinate(anchorRow, anchorColumn))
		{ }

		public SelectedTargetPattern(ITargetingRule rule, ITargetPattern pattern, int team, Coordinate anchor)
		{
			Rule = rule;
			Pattern = pattern;
			Team = team;
			Anchor = anchor;
		}

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntity user, IBattleModel battleModel)
			=> battleModel.Entities.Where(e =>
				Pattern.ContainsLocation(Anchor, e.Position) &&
				e.Team == Team &&
				Rule.IsValidTarget(user, e));
	}
}
