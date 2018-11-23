using System;
using System.Diagnostics;
using System.Threading;
using Davfalcon.Revelator.Borger;
using Redninja.Data;
using Redninja.Logging;
using Redninja.Presenter;
using static Redninja.Data.Encounter;

namespace Redninja.ConsoleDriver
{
	class Program
	{
		private const string CONFIG_FILE_PATH = "Assets/Data/config.json";

		static void Main(string[] args)
		{
			InstallLogger();
			ConsoleView view = new ConsoleView();

			IDataManager dataManager = DataManagerFactory.Create(CONFIG_FILE_PATH);

			IBattlePresenter presenter = BattlePresenter.CreatePresenter(
				view,
				builder => builder
					.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
					.AddDamageResist(DamageType.Physical, CombatStats.DEF)
					.SetDefaultDamageResource(CombatStats.HP)
					.AddVolatileStat(CombatStats.HP));

			const int playerTeam = 0;
			const int enemyTeam = 1;

			presenter.Configure(config =>
			{									
				Encounter encounter = dataManager.Encounters["goblin_party"];

				// environment
				config.SetTeamGrid(playerTeam, encounter.PlayerGridSize);
				config.SetTeamGrid(enemyTeam, encounter.EnemyGridSize);

				// players
				config.AddPlayerCharacter(TestablePlayerFactory.WarriorUnit(dataManager), 
					playerTeam, 
					new Coordinate(0, 0),
					TestablePlayerFactory.WarriorSkills(dataManager));

				// enemies
				foreach (EnemyMeta enemyMeta in encounter.EnemyMetas)
				{					
					config.AddAICharacter(enemyMeta.Character, 
						enemyTeam, 
						enemyMeta.InitialPosition, 
						enemyMeta.AiBehavior,
						enemyMeta.DisplayName);
				}
			});

			presenter.Initialize();
			Console.WriteLine("Press any key to start presenter clock...");
			Console.ReadKey();
			presenter.Start();

			while (true)
			{
				presenter.IncrementGameClock(0.2f);
				view.Draw();
				Thread.Sleep(100);
			}
		}

		private static void InstallLogger()
		{
			RLog.AppendPrinter((string tag, object msg, RLog.LogType logtype) =>
			{
				string text = $"[{tag}] {msg}";
				switch (logtype)
				{
					case RLog.LogType.ERROR:
						Debug.Fail(text);
						break;

					default:
						Debug.WriteLine(text);
						break;
				}
			});
		}
	}
}
