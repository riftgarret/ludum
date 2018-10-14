using System.Collections.Generic;
using Redninja.Skills;

namespace Redninja.Decisions
{
	public interface IActionPhaseHelper
	{
		IBattleEntity Entity { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
