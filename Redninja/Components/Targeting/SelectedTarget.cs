using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTarget : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public IEntityModel Target { get; }

		ITargetPattern ISelectedTarget.Pattern => null;
		int ISelectedTarget.Team => Target.Team;
		Coordinate ISelectedTarget.Anchor => Target.Position;

		public SelectedTarget(ITargetingRule rule, IEntityModel target)
		{
			Rule = rule;
			Target = target;
		}

		public IEnumerable<IEntityModel> GetValidTargets(IEntityModel user, IBattleModel battleModel)
		{
			List<IEntityModel> list = new List<IEntityModel>();
			if (Rule.IsValidTarget(user, Target))
				list.Add(Target);
			return list;
		}
	}
}
