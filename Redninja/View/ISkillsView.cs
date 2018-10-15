using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.View
{
	public interface ISkillsView
	{
		IEntityModel Entity { get; }
		IWeaponAttack Attack { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
