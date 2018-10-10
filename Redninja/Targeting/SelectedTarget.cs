using System.Collections.Generic;

namespace Redninja.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTarget : ITarget
	{
		public ITargetingRule Rule { get; }
		public IBattleEntity Target { get; }

		public SelectedTarget(ITargetingRule rule, IBattleEntity target)
		{
			Rule = rule;
			Target = target;
		}
	}
}
