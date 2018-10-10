using System.Collections.Generic;

namespace Redninja.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTarget : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public IBattleEntity Target { get; }

		public SelectedTarget(ITargetingRule rule, IBattleEntity target)
		{
			Rule = rule;
			Target = target;
		}

		public IEnumerable<IBattleEntity> GetValidTargets(IBattleEntityManager entityManager)
		{
			List<IBattleEntity> list = new List<IBattleEntity>();
			if (Rule.IsValidTarget(Target))
				list.Add(Target);
			return list;
		}
	}
}
