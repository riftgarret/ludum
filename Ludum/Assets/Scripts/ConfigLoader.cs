using System.IO;
using Redninja;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Data;
using Redninja.Entities;
using Redninja.Logging;
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
		RLog.AppendPrinter((tag, msg, type) => Debug.Log($"{tag}: {msg}"));

		string path = Path.Combine(Application.streamingAssetsPath, configPath);
		IDataManager dataManager = DataManagerFactory.Create(path);
		
		context = new BattleContext(dataManager);
		context.OnCombatEvent += (e) => RLog.D(this, e);

		var bem = context.Get<IBattleEntityManager>();

		Encounter encounter = dataManager.CreateInstance<Encounter>("goblin_solo");

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

		var val = dataManager.CreateInstance<IAIRule>("fire_enemy2");
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
			unit.Name = "Davfalcon";
			unit.CoreStats[Stat.HP] = 100;
			unit.CoreStats[Stat.Mana] = 50;
			unit.CoreStats[Stat.Stamina] = 50;
			return unit;
		}
			

		public static ISkillProvider WarriorSkills(IDataManager dataManager)
		{
			ConfigurableSkillProvider skillProvider = new ConfigurableSkillProvider();
			skillProvider.Skills.Add(dataManager.SingleInstance<ISkill>("fire_skill"));
			skillProvider.Skills.Add(dataManager.SingleInstance<ISkill>("bleed_debuff"));
			skillProvider.AttackTime = new ActionTime(1, 2, 1);
			return skillProvider;
		}
	}
}
