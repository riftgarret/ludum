using System.Collections.Generic;

namespace Redninja.Components.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	internal class SelectedTarget : ISelectedTarget
	{
		public ITargetingRule Rule { get; }
		public IUnitModel Target { get; }

		ITargetPattern ISelectedTarget.Pattern => null;
		int ISelectedTarget.Team => Target.Team;
		Coordinate ISelectedTarget.Anchor => Target.Position;

		public SelectedTarget(ITargetingRule rule, IUnitModel target)
		{
			Rule = rule;
			Target = target;
		}

		public IEnumerable<IUnitModel> GetValidTargets(IUnitModel user, IBattleModel battleModel)
		{
			List<IUnitModel> list = new List<IUnitModel>();
			if (Rule.IsValidTarget(user, Target))
				list.Add(Target);
			return list;
		}
	}
}
