using Redninja.Components.Combat;

namespace Redninja.Components.Skills
{
	public interface ISkillResolver
	{
		bool Resolved { get; }
		float ExecutionStart { get; }

		IBattleOperation Resolve(IBattleEntity entity);
	}
}
