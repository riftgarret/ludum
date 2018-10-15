﻿using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Entities.Decisions
{
	public interface IActionPhaseHelper
	{
		IBattleEntity Entity { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}