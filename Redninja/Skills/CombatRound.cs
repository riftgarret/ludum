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
	}
}
