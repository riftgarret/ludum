using Redninja.Targeting;

namespace Redninja.Skills
{
	public class SkillExecutionTrigger
	{
		public delegate IBattleOperation OperationProvider(IBattleEntity entity, ISelectedTarget target, ICombatSkill skill);

		public float ExecutionStart { get; }
		public ITargetPattern Pattern { get; }
		public OperationProvider GetOperation { get; }

		public SkillResolver GetResolver(ISelectedTarget target)
			=> new SkillResolver(this, target);
	}
}
