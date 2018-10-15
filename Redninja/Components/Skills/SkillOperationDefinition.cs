using System;
using Redninja.Components.Operations;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class SkillOperationDefinition
	{
		public delegate IBattleOperation OperationProvider(IEntityModel entity, ITargetResolver target, ISkill skill);

		public float ExecutionStart { get; }
		public ITargetPattern Pattern { get; }
		public OperationProvider GetOperation { get; }

		public ISkillResolver GetResolver(ITargetResolver target)
			=> new SkillOperationResolver(this, target);

		public SkillOperationDefinition(float executionStart, OperationProvider getOperation)
			: this(executionStart, null, getOperation)
		{ }

		public SkillOperationDefinition(float executionStart, ITargetPattern pattern, OperationProvider getOperation)
		{
			ExecutionStart = executionStart;
			Pattern = pattern;
			GetOperation = getOperation ?? throw new ArgumentNullException(nameof(getOperation));
		}
	}
}
