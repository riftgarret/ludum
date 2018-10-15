using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Entities.Decisions
{
	public interface IActionPhaseHelper
	{
		IEntityModel Entity { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
