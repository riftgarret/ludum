using Redninja.Components.Operations;

namespace Redninja.Components.Skills
{
	public interface ISkillResolver
	{
		bool Resolved { get; }
		float ExecutionStart { get; }

		IBattleOperation Resolve(IUnitModel entity, ISkill skill);
	}
}
