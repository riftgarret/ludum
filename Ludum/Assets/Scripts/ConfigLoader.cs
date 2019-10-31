using System.IO;
using Redninja;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Skills;
using Redninja.Data;
using Redninja.Entities;
using Redninja.Presenter;
using UnityEngine;
using static Redninja.Data.Encounter;

public class ConfigLoader : MonoBehaviour
{
	public string configPath;

	private IBattleContext context;

	void Awake()
	{
		LoadGame();
	}

	void LoadGame()
	{
		string path = Path.Combine(Application.streamingAssetsPath, configPath);
		IDataManager dataManager = DataManagerFactory.Create(path);

		ICombatExecutor combatExecutor = new CombatExecutor();

		context = new BattleContext(dataManager, combatExecutor);

		var bem = context.Get<IBattleEntityManager>();

		Encounter encounter = dataManager.CreateInstance<Encounter>("goblin_party");

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
		{
			Unit unit = new Unit();
			unit.BaseStats[Stat.HP] = 100;
			unit.BaseStats[Stat.ATK] = 20;
			unit.BaseStats[Stat.Resource] = 200;
			return unit;
		}
			

		public static ISkillProvider WarriorSkills(IDataManager dataManager)
		{
			ConfigurableSkillProvider skillProvider = new ConfigurableSkillProvider();
			skillProvider.Skills.Add(dataManager.SingleInstance<ISkill>("fire_skill"));
			//skillProvider.Skills.Add(dataManager.SingleInstance<ISkill>("multi_hit"));
			skillProvider.AttackTime = new ActionTime(1, 2, 1);
			return skillProvider;
		}
	}
}
