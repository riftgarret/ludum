using System;
using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Components.Classes
{
	public interface IClassDefinition
	{
		string DisplayName { get; }
		IStatScaler LevelScaler { get; }
		IEnumerable<Tuple<IClassPerk, ICharacteristicRequirement>> Perks { get; }
		IEnumerable<Tuple<ISkill, ICharacteristicRequirement>> Skills { get; }
	}
}
