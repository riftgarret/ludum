using System;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class CombatRound
	{
		public delegate IBattleOperation OperationProvider(IBattleEntity entity, ITargetResolver target, ICombatSkill skill);

		public float ExecutionStart { get; }
		public ITargetPattern Pattern { get; }
		public OperationProvider GetOperation { get; }

		public SkillResolver GetResolver(ITargetResolver target)
			=> new SkillResolver(this, target);

		public CombatRound(float executionStart, OperationProvider getOperation)
			: this(executionStart, null, getOperation)
		{ }

		public CombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation)
		{
			ExecutionStart = executionStart;
			Pattern = pattern;
			GetOperation = getOperation ?? throw new ArgumentNullException(nameof(getOperation));
		}
	}
}
