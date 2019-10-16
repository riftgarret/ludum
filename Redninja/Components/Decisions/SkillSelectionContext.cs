﻿using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Equipment;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Result for requesting what available skills a entity can use.
	/// </summary>
	internal class SkillSelectionContext : IActionContext
	{
		public IBattleEntity Entity { get; }
		public IWeaponAttack Attack { get; }
		public IEnumerable<ISkill> Skills { get; }

		public SkillSelectionContext(IBattleEntity entity, ISkillProvider skillProvider)
		{
			Entity = entity;
			Attack = skillProvider.GetAttack(entity.GetEquipmentManager().GetAllEquipmentOfType(EquipmentType.Weapon).Select(e => e as IWeapon));
			Skills = skillProvider.GetSkills();
		}
	}
}
