using System;
using System.Diagnostics;
using System.Threading;
using Davfalcon;
using Davfalcon.Nodes;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Combat;
using Redninja.Entities;
using Redninja.Events;

namespace Redninja.ConsoleDriver
{
	class Program
	{
		static void Main(string[] args)
		{
			ICombatExecutor executor = new CombatExecutor(builder => builder
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.AddVolatileStat(CombatStats.HP));

			IBattlePresenter presenter = new BattlePresenter(new ConsoleView(), executor);
			IBattleEntity unit1 = new BattleEntity(new Unit.Builder(StatsOperations.Default, LinkedStatsResolver.Default)
				.SetMainDetails("Unit 1")
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.ATK, 50)
				.SetBaseStat(CombatStats.DEF, 10)
				.Build(), new PlayerInput(), executor);
			IBattleEntity enemy = new BattleEntity(new Unit.Builder()
				.SetMainDetails("Enemy 1")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 10)
				.Build(), new DummyAI(), executor);

			presenter.AddBattleEntity(unit1);
			presenter.AddBattleEntity(enemy);
			presenter.BattleEventOccurred += OnBattleEvent;

			presenter.Initialize();
			Console.ReadKey();
			while (true)
			{
				Console.Clear();
				presenter.IncrementGameClock(0.2f);
				presenter.Update();
				Thread.Sleep(100);
			}
		}

		private static void OnBattleEvent(IBattleEvent battleEvent)
		{
			Debug.WriteLine("Battle event occured");
			if (battleEvent is MovementEvent me)
			{
				Debug.WriteLine($"{me.Entity.Character.Name} moved to ({me.NewPosition.Row},{me.NewPosition.Column})");
			}
			else if (battleEvent is DamageEvent de)
			{
				Debug.Write(de.Damage.ToStringRecursive());
			}
		}
	}
}
