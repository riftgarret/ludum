using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	public interface IActionContext
	{
		IBattleEntity Entity { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
