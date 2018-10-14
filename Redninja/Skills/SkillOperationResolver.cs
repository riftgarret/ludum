using System;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class SkillOperationResolver : ISkillResolver
	{
		private readonly SkillOperationDefinition definition;
		private readonly ITargetResolver target;

		public float ExecutionStart => definition.ExecutionStart;
		public bool Resolved { get; private set; } = false;

		public SkillOperationResolver(SkillOperationDefinition definition, ITargetResolver target)
		{
			this.definition = definition ?? throw new ArgumentNullException(nameof(definition));
			this.target = target;
		}

		public IBattleOperation Resolve(IBattleEntity entity, ISkill skill)
		{
			Resolved = true;
			return definition.GetOperation(entity, target, skill);
		}
	}
}
