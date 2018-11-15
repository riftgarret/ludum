using Davfalcon.Revelator;
using Redninja.Components.Skills;

namespace Redninja.System
{
	public interface ISystemProvider
	{
		IClassProvider GetClass(string className);

		ISkillProvider GetSkillProvider(IUnitModel unit);
	}
}
