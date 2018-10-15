using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.View
{
	public interface ISkillsView
	{
		IUnitModel Entity { get; }
		IWeaponAttack Attack { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
