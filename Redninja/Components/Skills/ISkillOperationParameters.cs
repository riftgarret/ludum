using System;
using System.Collections.Generic;
using Davfalcon;
﻿using Redninja.Components.Combat;

namespace Redninja.Components.Skills
{
	public interface ISkillOperationParameters
	{
		IStats Stats { get; }
		ISet<Enum> SkillFlags { get; }
	}
}
