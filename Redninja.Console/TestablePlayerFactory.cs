﻿using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.ConsoleDriver.Objects;
using Redninja.Data;
using Redninja.System;

namespace Redninja.ConsoleDriver
{
	public static class TestablePlayerFactory
	{
		public static IUnit WarriorUnit(IDataManager dataManager)
			=> Unit.Build(b => b
			.SetMainDetails("Unit 1", "warrior")
			.SetBaseStat(CombatStats.STR, 16)
			.SetBaseStat(CombatStats.CON, 14)
			.SetBaseStat(CombatStats.AGI, 12)
			.SetBaseStat(CombatStats.DEX, 11)
			.SetBaseStat(CombatStats.INT, 9)
			.SetBaseStat(CombatStats.WIS, 9)
			.SetBaseStat(CombatStats.CHA, 8)
			.SetBaseStat(CombatStats.LUK, 13)
			.SetBaseStat(CombatStats.Level, 5)
			.SetBaseStat(CombatStats.HpLevelScale, 20)
			.SetBaseStat(CombatStats.HP, 100)
			.SetBaseStat(CombatStats.ATK, 50)
			.SetBaseStat(CombatStats.DEF, 10)			
			.AddEquipmentSlot(EquipmentType.Weapon)
			.AddEquipment(Weapons.Sword));

		public static ISkillProvider WarriorSkills(IDataManager dataManager)
		{
			ConfigurableSkillProvider skillProvider = new ConfigurableSkillProvider();
			skillProvider.Skills.Add(dataManager.Skills["double_hit"]);
			skillProvider.Skills.Add(dataManager.Skills["multi_hit"]);
			skillProvider.AttackTime = new ActionTime(1, 2, 1);
			return skillProvider;
		}
	}	
}