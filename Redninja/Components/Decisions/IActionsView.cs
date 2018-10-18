using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	public interface IActionsView
	{
		IUnitModel Entity { get; }
		IWeaponAttack Attack { get; }
		IEnumerable<ISkill> Skills { get; }
	}
}
