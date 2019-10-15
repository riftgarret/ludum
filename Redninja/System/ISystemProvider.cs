using Davfalcon.Revelator;
using Redninja.Components.Skills;

namespace Redninja.System
{
	public interface ISystemProvider
	{
		IClassProvider GetClass(string className);

		// TODO this should be attached to IUnit 
		ISkillProvider GetSkillProvider(IBattleEntity unit);

		void SetSkillProvider(IBattleEntity unit, ISkillProvider provider);
	}
}
