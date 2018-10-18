using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Davfalcon.Nodes;
using Redninja.Components.Decisions;
using Redninja.ConsoleDriver.Objects;
using Redninja.Events;
using Redninja.View;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		private readonly Mutex mutex = new Mutex();
		private IBattleModel model;
		private IUnitModel waiting;

		private Action checkActionNeeded;
		private Action drawUnitActions;
		private Action drawTargeting;

		public void Draw()
		{
			mutex.WaitOne();
			Console.Clear();
			foreach (IUnitModel entity in model.Entities)
			{
				entity.Print();
			}

			if (waiting != null)
			{
			}

			checkActionNeeded?.Invoke();
			drawUnitActions?.Invoke();
			drawTargeting?.Invoke();
			mutex.ReleaseMutex();
		}

		private void Safe(Action func)
		{
			mutex.WaitOne();
			func();
			mutex.ReleaseMutex();
		}

		public void SetBattleModel(IBattleModel model)
		{
			this.model = model;
		}

		public void OnDecisionNeeded(IUnitModel entity)
		{
			Debug.WriteLine("Waiting for player input...");

			Safe(() => waiting = entity);
		}

		public void Resume()
		{
			Debug.WriteLine("Resuming execution.");

			Safe(() => waiting = null);
		}

		public void SetViewMode(IBaseCallbacks callbacks)
		{
			Debug.WriteLine("Entering default view state.");

			Safe(() =>
			{
				checkActionNeeded = () =>
				{
					if (waiting != null)
						callbacks.SelectUnit(waiting);
				};
				drawUnitActions = null;
				drawTargeting = null;
			});
		}

		public void SetViewMode(IActionsView actionsContext, ISkillsCallbacks callbacks)
		{
			Debug.WriteLine("Entering action selection view state.");

			IUnitModel entity = actionsContext.Entity;
			Safe(() =>
			{
				checkActionNeeded = null;
				drawUnitActions = () =>
				{
					Console.WriteLine($"Select an action for {entity.Character.Name}...");
				};
				drawTargeting = null;
			});

			new Thread(() =>
			{
				ConsoleKey key = Console.ReadKey().Key;
				switch (key)
				{
					case ConsoleKey.A:
						callbacks.SelectSkill(entity, CombatSkills.TwoHandedAttack);
						break;
					case ConsoleKey.M:
						callbacks.InitiateMovement(entity);
						break;
					case ConsoleKey.S:
						callbacks.SelectSkill(entity, CombatSkills.PatternSkill);
						break;
					default:
						callbacks.Wait(entity);
						break;
				}
			}).Start();
		}

		public void SetViewMode(IMovementView movementContext, IMovementCallbacks callbacks)
		{
			Debug.WriteLine("Entering movement view state.");

			throw new NotImplementedException();
		}

		public void SetViewMode(ITargetingView targetingContext, ITargetingCallbacks callbacks)
		{
			Debug.WriteLine("Entering targeting view state.");

			List<IUnitModel> availableTargets = new List<IUnitModel>(targetingContext.GetTargetableEntities());
			Safe(() =>
			{
				checkActionNeeded = null;
				drawUnitActions = null;
				drawTargeting = () =>
				{

					foreach (IUnitModel t in availableTargets)
					{
						Console.WriteLine(t.Character.Name);
					}

					Console.WriteLine("Select target...");
				};
			});

			new Thread(() =>
			{
				try
				{
					int selected = Convert.ToInt32(Console.ReadKey().KeyChar.ToString()) - 1;
					callbacks.SelectTarget(targetingContext.GetSelectedTarget(availableTargets[selected]));
				}
				catch (Exception)
				{
					callbacks.Cancel();
				}
			}).Start();
		}

		public void OnBattleEventOccurred(IBattleEvent battleEvent)
		{
			Debug.WriteLine("Battle event occurred.");
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
