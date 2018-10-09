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
			ICombatExecutor executor = new CombatExecutor(builder => builder
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.AddVolatileStat(CombatStats.HP));

			IBattlePresenter presenter = BattlePresenter.CreatePresenter(new ConsoleView(), executor);
			presenter.AddCharacter(
				new Unit.Builder(StatsOperations.Default, LinkedStatsResolver.Default)
				.SetMainDetails("Unit 1")
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.ATK, 50)
				.SetBaseStat(CombatStats.DEF, 10),
				new PlayerInput(), 0, 0);
			presenter.AddCharacter(
				new Unit.Builder()
				.SetMainDetails("Enemy 1")
				.SetBaseStat(CombatStats.HP, 1000)
				.SetBaseStat(CombatStats.DEF, 10),
				new DummyAI(), 3, 0);

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
