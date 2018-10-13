namespace Redninja.Skills
{
	public interface ISkillResolver
	{
		bool Resolved { get; }
		float ExecutionStart { get; }

		IBattleOperation Resolve(IBattleEntity entity, ISkill skill);
	}
}