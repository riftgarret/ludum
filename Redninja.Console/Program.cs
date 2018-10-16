using System;
using System.Threading;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Combat;
using Redninja.Components.Skills;
using Redninja.ConsoleDriver.Objects;
using Redninja.Data;
using Redninja.Presenter;

namespace Redninja.ConsoleDriver
{
	class Program
	{
		private const string CONFIG_FILE_PATH = "Assets/Data/config.json";

		static void Main(string[] args)
		{
			DataManager manager = new DataManager();
			manager.LoadJson(CONFIG_FILE_PATH);

			// This serves no purpose, it's just here to prove it works
			manager.Load(new ObjectLoader<ISkill>(typeof(CombatSkills)));
			manager.Load(new ObjectLoader<IWeapon>(typeof(Weapons)));

			ConsoleView view = new ConsoleView();

			ICombatExecutor executor = new CombatExecutor(builder => builder
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.AddVolatileStat(CombatStats.HP));

			IBattlePresenter presenter = BattlePresenter.CreatePresenter(view, executor);
			presenter.AddCharacter(b => b
				.SetMainDetails("Unit 1")
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.ATK, 50)
				.SetBaseStat(CombatStats.DEF, 10),
				0, 0);
			presenter.AddCharacter(b => b
				.SetMainDetails("Enemy 1")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 10),
				new DummyAI(), 1, 0, 0);
			presenter.AddCharacter(b => b
				.SetMainDetails("Enemy 2")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 20),
				new DummyAI(), 1, 1, 0);
			presenter.AddCharacter(b => b
				.SetMainDetails("Enemy 3")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 30),
				new DummyAI(), 1, 2, 0);

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
	}
}
