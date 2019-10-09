using System.Collections;
using System.Collections.Generic;
using System.IO;
using Davfalcon.Revelator.Borger;
using Redninja;
using Redninja.Data;
using Redninja.Presenter;
using UnityEditor;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
	public string configPath;

	void Awake()
	{

	}

	void todo() { 
		BattleView view = GetComponent<BattleView>();

		string path = Path.Combine(Application.streamingAssetsPath, configPath);
		IDataManager dataManager = DataManagerFactory.Create(path);
		

		IBattlePresenter presenter = BattlePresenter.CreatePresenter(
			view,
			builder => builder
				.AddDamageScaling(DamageType.Physical, Redninja.CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, Redninja.CombatStats.DEF)
				.SetDefaultDamageResource(Redninja.CombatStats.HP)
				.AddVolatileStat(Redninja.CombatStats.HP));

		const int playerTeam = 0;
		const int enemyTeam = 1;

		//presenter.Configure(config =>
		//{
		//	Encounter encounter = dataManager.Encounters["goblin_party"];

		//	// environment
		//	config.SetTeamGrid(playerTeam, encounter.PlayerGridSize);
		//	config.SetTeamGrid(enemyTeam, encounter.EnemyGridSize);

		//	// players
		//	config.AddPlayerCharacter(TestablePlayerFactory.WarriorUnit(dataManager),
		//		playerTeam,
		//		new Coordinate(0, 0),
		//		TestablePlayerFactory.WarriorSkills(dataManager));

		//	// enemies
		//	foreach (EnemyMeta enemyMeta in encounter.EnemyMetas)
		//	{
		//		config.AddAICharacter(enemyMeta.Character,
		//			enemyTeam,
		//			enemyMeta.InitialPosition,
		//			enemyMeta.AiBehavior,
		//			enemyMeta.DisplayName);
		//	}
		//});

		//presenter.Initialize();
		//Console.WriteLine("Press any key to start presenter clock...");
		//Console.ReadKey();
		//presenter.Start();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
