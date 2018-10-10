using System;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class SkillResolver : ISkillResolver
	{
		private readonly CombatRound definition;
		private readonly ISelectedTarget target;

		public float ExecutionStart => definition.ExecutionStart;
		public bool Resolved { get; private set; } = false;

		public SkillResolver(CombatRound definition, ISelectedTarget target)
		{
			this.definition = definition ?? throw new ArgumentNullException(nameof(definition));
			this.target = target;
		}

		public IBattleOperation Resolve(IBattleEntity entity, ICombatSkill skill)
		{
			Resolved = true;
			return definition.GetOperation(entity, target, skill);
		}
	}
}
