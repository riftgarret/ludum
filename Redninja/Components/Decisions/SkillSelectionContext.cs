using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Result for requesting what available skills a entity can use.
	/// </summary>
	public class SkillSelectionContext : IActionsContext
	{
		public IUnitModel Entity { get; }
		public IWeaponAttack Attack { get; }
		public IEnumerable<ISkill> Skills { get; }

		public SkillSelectionContext(IUnitModel entity)
		{
			Entity = entity;
		}

		// SkillProvider needs to be implemented before we can use this
		public SkillSelectionContext(IUnitModel entity, ISkillProvider skillProvider)
			: this(entity)
		{
			Attack = skillProvider.GetAttack(entity.Character.Class, entity.Character.Equipment.GetAllEquipmentForSlot(EquipmentType.Weapon).Select(e => e as IWeapon));
			Skills = new List<ISkill>(skillProvider.GetSkills(entity.Character.Class, entity.Character.Level));
		}
	}
}
