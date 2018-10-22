﻿using System;
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

			IBattlePresenter presenter = BattlePresenter.CreatePresenter(view, builder => builder
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.AddVolatileStat(CombatStats.HP));

			presenter.Configure(config =>
			{
				config.AddCharacter(b => b
					.SetMainDetails("Unit 1")
					.SetBaseStat(CombatStats.HP, 100)
					.SetBaseStat(CombatStats.ATK, 50)
					.SetBaseStat(CombatStats.DEF, 10),
					0, 0);
				config.AddCharacter(b => b
					.SetMainDetails("Enemy 1")
					.SetBaseStat(CombatStats.HP, 1000)
					.SetBaseStat(CombatStats.DEF, 10),
					new DummyAI(), 1, 0, 0);
				config.AddCharacter(b => b
					.SetMainDetails("Enemy 2")
					.SetBaseStat(CombatStats.HP, 1000)
					.SetBaseStat(CombatStats.DEF, 20),
					new DummyAI(), 1, 1, 0);
				config.AddCharacter(b => b
					.SetMainDetails("Enemy 3")
					.SetBaseStat(CombatStats.HP, 1000)
					.SetBaseStat(CombatStats.DEF, 30),
					new DummyAI(), 1, 2, 0);
				config.SetTeamGrid(0, new Coordinate(3, 3));
				config.SetTeamGrid(1, new Coordinate(3, 3));
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
	}
}
