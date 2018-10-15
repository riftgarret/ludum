using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Components.Actions.Decisions
{
	public interface IActionPhaseHelper
	{
		IBattleEntity Entity { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
