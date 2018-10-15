using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTarget : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public IBattleEntity Target { get; }

		ITargetPattern ISelectedTarget.Pattern => null;
		int ISelectedTarget.Team => Target.Team;
		Coordinate ISelectedTarget.Anchor => Target.Position;

		public SelectedTarget(ITargetingRule rule, IBattleEntity target)
		{
			Rule = rule;
			Target = target;
		}

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntity user, IBattleEntityManager entityManager)
		{
			List<IBattleEntity> list = new List<IBattleEntity>();
			if (Rule.IsValidTarget(user, Target))
				list.Add(Target);
			return list;
		}
	}
}
