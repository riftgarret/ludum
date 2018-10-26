using System;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class SkillOperationDefinition
	{
		private readonly ISkillOperationParameters args;
		private readonly OperationProvider operationProvider;

		public float ExecutionStart { get; }
		public ITargetPattern Pattern { get; }

		private class Resolver : ISkillResolver
		{
			private readonly SkillOperationDefinition definition;
			private readonly ITargetResolver target;

			public float ExecutionStart => definition.ExecutionStart;
			public bool Resolved { get; private set; } = false;

			public Resolver(SkillOperationDefinition definition, ITargetResolver target)
			{
				this.definition = definition ?? throw new ArgumentNullException(nameof(definition));
				this.target = target;
			}

			public IBattleOperation Resolve(IUnitModel entity)
			{
				Resolved = true;
				return definition.operationProvider(entity, target, definition.args);
			}
		}

		public ISkillResolver GetResolver(ITargetResolver target)
			=> new Resolver(this, target);

		internal SkillOperationDefinition(float executionStart, OperationProvider operationProvider, ISkillOperationParameters args)
			: this(executionStart, null, operationProvider, args)
		{ }

		internal SkillOperationDefinition(float executionStart, ITargetPattern pattern, OperationProvider operationProvider, ISkillOperationParameters args)
		{
			this.args = args;
			this.operationProvider = operationProvider ?? throw new ArgumentNullException(nameof(operationProvider));
			ExecutionStart = executionStart;
			Pattern = pattern;
		}
	}
}
