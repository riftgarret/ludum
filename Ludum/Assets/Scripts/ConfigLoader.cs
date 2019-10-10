using System.IO;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Skills;
using Redninja.Data;
using Redninja.Entities;
using Redninja.Presenter;
using UnityEngine;
using static Redninja.Data.Encounter;
using IUnit = Davfalcon.Revelator.IUnit;

public class ConfigLoader : MonoBehaviour
{
	public string configPath;

	private IBattleContext context;

	void Awake()
	{
		LoadGame();
	}

	void LoadGame() { 		
		string path = Path.Combine(Application.streamingAssetsPath, configPath);
		IDataManager dataManager = DataManagerFactory.Create(path);

		ICombatExecutor combatExecutor = new CombatExecutor(x => x.
					AddDamageScaling(CalculatedStat.PhysicalDamage, Stat.ATK)
					.AddDamageResist(CalculatedStat.PhysicalDamage, Stat.DEF)
					.SetDefaultDamageResource(CalculatedStat.HP)
					.AddVolatileStat(CalculatedStat.Resource)
					.AddVolatileStat(CalculatedStat.HP));

		context = new BattleContext(dataManager, combatExecutor);

		var bem = context.Get<IBattleEntityManager>();

		Encounter encounter = dataManager.Encounters["goblin_party"];

		const int playerTeam = 0;
		const int enemyTeam = 1;

		// environment
		bem.SetGrid(playerTeam, encounter.PlayerGridSize);
		bem.SetGrid(enemyTeam, encounter.EnemyGridSize);

		// players
		bem.AddPlayerCharacter(TestablePlayerFactory.WarriorUnit(dataManager),
			playerTeam,
			new Coordinate(0, 0),
			TestablePlayerFactory.WarriorSkills(dataManager));

		// enemies
		foreach (EnemyMeta enemyMeta in encounter.EnemyMetas)
		{
			bem.AddAICharacter(enemyMeta.Character,
				enemyTeam,
				enemyMeta.InitialPosition,
				enemyMeta.AiBehavior,
				enemyMeta.DisplayName);
		}

		GetComponent<BattleView>().Initialize(context);		
	}
    
    void OnDestroy()
    {
		context.Dispose();
    }

	public static class TestablePlayerFactory
	{
		public static IUnit WarriorUnit(IDataManager dataManager)
			=> Unit.Build(b => b
			.SetMainDetails("Unit 1", "warrior")
			.SetBaseStat(Stat.STR, 16)
			.SetBaseStat(Stat.CON, 14)
			.SetBaseStat(Stat.AGI, 12)
			.SetBaseStat(Stat.DEX, 11)
			.SetBaseStat(Stat.INT, 9)
			.SetBaseStat(Stat.WIS, 9)
			.SetBaseStat(Stat.CHA, 8)
			.SetBaseStat(Stat.LUK, 13)
			.SetBaseStat(Stat.Level, 5)
			.SetBaseStat(Stat.HpLevelScale, 20)
			.SetBaseStat(Stat.HP, 100)
			.SetBaseStat(CalculatedStat.HP, 100) // for now
			.SetBaseStat(CalculatedStat.Resource, 100)
			.SetBaseStat(Stat.ATK, 50)
			.SetBaseStat(Stat.DEF, 10)
			.AddEquipmentSlot(EquipmentType.Weapon)
			.AddEquipment(Sword));

		public static ISkillProvider WarriorSkills(IDataManager dataManager)
		{
			ConfigurableSkillProvider skillProvider = new ConfigurableSkillProvider();
			skillProvider.Skills.Add(dataManager.Skills["double_hit"]);
			skillProvider.Skills.Add(dataManager.Skills["multi_hit"]);
			skillProvider.AttackTime = new ActionTime(1, 2, 1);
			return skillProvider;
		}

		public static IWeapon Sword { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
			.SetName("Longsword")
			.SetDamage(20)
			.AddDamageType(DamageType.Physical)
			.SetStatAddition(CombatStats.ATK, 10));

		public static IWeapon Shortsword { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
			.SetName("Shortsword")
			.SetDamage(10)
			.AddDamageType(DamageType.Physical));

		public static IWeapon Dagger { get; } = Weapon.Build(EquipmentType.Weapon, WeaponType.Dagger, b => b
			.SetName("Dagger")
			.SetDamage(5)
			.AddDamageType(DamageType.Physical));
	}
}
