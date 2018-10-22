using Redninja.Components.Skills;

namespace Redninja.System
{
	public interface IClassProvider
	{
		ISkillProvider GetSkillProvider(int level);
	}
}
