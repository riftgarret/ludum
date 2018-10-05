using System;
using System.Diagnostics;
using System.Threading;
using Davfalcon;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Combat;
using Redninja.BattleSystem;
using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Events;

namespace Redninja.ConsoleDriver
{
	class Program
	{
		static void Main(string[] args)
		{
			ICombatResolver resolver = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.Build();

			IBattlePresenter presenter = new BattlePresenter(new ConsoleView(), null);
			IBattleEntity unit1 = new BattleEntity(new Unit.Builder(StatsOperations.Default, LinkedStatsResolver.Default)
				.SetMainDetails("Unit 1")
				.SetAllBaseStats<Attributes>(10)
				.Build(), resolver)
			{
				ActionDecider = new PlayerInput()
			};
			IBattleEntity enemy = new BattleEntity(new Unit.Builder()
				.SetMainDetails("Enemy 1")
				.SetAllBaseStats<Attributes>(10)
				.Build(), resolver);

			presenter.AddBattleEntity(unit1);
			presenter.BattleEventOccurred += OnBattleEvent;

			presenter.Initialize();
			Console.ReadKey();
			while (true)
			{
				presenter.ProcessBattleOperationQueue();
				presenter.IncrementGameClock(0.2f);
				Console.Clear();
				Console.WriteLine(unit1.Print());
				presenter.ProcessDecisionQueue();
				Thread.Sleep(100);
			}
		}

		private static void OnBattleEvent(IBattleEvent battleEvent)
		{
			Debug.WriteLine("Battle event occured");
			if (battleEvent is MovementEvent e)
			{
				Console.WriteLine($"{e.Entity} moved to ({e.NewPosition.Row},{e.NewPosition.Column})");
				Console.ReadKey();
			}
		}
	}
}
