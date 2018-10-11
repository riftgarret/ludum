using System;
using System.Diagnostics;
using System.Threading;
using Davfalcon;
using Davfalcon.Nodes;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Events;

namespace Redninja.ConsoleDriver
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleView view = new ConsoleView();

			ICombatExecutor executor = new CombatExecutor(builder => builder
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.AddVolatileStat(CombatStats.HP));

			IBattlePresenter presenter = BattlePresenter.CreatePresenter(view, executor);
			presenter.AddCharacter(
				new Unit.Builder(StatsOperations.Default, LinkedStatsResolver.Default)
				.SetMainDetails("Unit 1")
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.ATK, 50)
				.SetBaseStat(CombatStats.DEF, 10),
				0, 0);
			presenter.AddCharacter(
				new Unit.Builder()
				.SetMainDetails("Enemy 1")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 10),
				new DummyAI(), 1, 0, 0);
			presenter.AddCharacter(
				new Unit.Builder()
				.SetMainDetails("Enemy 2")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 20),
				new DummyAI(), 1, 1, 0);
			presenter.AddCharacter(
				new Unit.Builder()
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
