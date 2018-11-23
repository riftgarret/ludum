using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Davfalcon.Nodes;
using Redninja.Components.Combat.Events;
using Redninja.Components.Decisions;
using Redninja.Components.Skills;
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

			ActionHandler<IActionsView, ISkillsCallbacks> actionHandler = new ActionHandler<IActionsView, ISkillsCallbacks>();
			actionHandler.AddAction("Wait", (ac, cb) => cb.Wait(ac.Entity));
			actionHandler.AddAction($"Attack ({String.Join(", ", actionsContext.Attack.Weapons.Select(w => w.Name))})", 
				(ac, cb) => cb.SelectSkill(ac.Entity, ac.Attack));
			actionsContext.Skills.ToList().ForEach(
				skill => actionHandler.AddAction(skill.Name,
				(ac, cb) => cb.SelectSkill(ac.Entity, skill)));

			IUnitModel entity = actionsContext.Entity;
			Safe(() =>
			{
				checkActionNeeded = null;
				drawUnitActions = () =>
				{					
					Console.WriteLine($"\nSelect an action for {entity.Name}...");
					actionHandler.PrintActions();										
				};
				drawTargeting = null;
			});

			new Thread(() =>
			{
				Action<IActionsView, ISkillsCallbacks> action;
				while (!actionHandler.TryGetAction(Console.ReadKey(), out action))
					;
				Safe(() => action(actionsContext, callbacks));
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
			ActionHandler<ITargetingView, ITargetingCallbacks> actionHandler = new ActionHandler<ITargetingView, ITargetingCallbacks>();

			actionHandler.AddAction("Cancel Action", (tc, cb) => cb.Cancel());
			targetingContext.GetTargetableEntities().ToList().ForEach(
				x => actionHandler.AddAction(x.Name, 
				(tc, cb) => callbacks.SelectTarget(tc.GetSelectedTarget(x))));

			Safe(() =>
			{
				checkActionNeeded = null;
				drawUnitActions = null;
				drawTargeting = () =>
				{					
					Console.WriteLine("\nSelect target...");
					actionHandler.PrintActions();
				};
			});

			new Thread(() =>
			{
				Action<ITargetingView, ITargetingCallbacks> action;
				while (!actionHandler.TryGetAction(Console.ReadKey(), out action))
					;
				Safe(() => action(targetingContext, callbacks));
			}).Start();
		}

		public void OnBattleEventOccurred(ICombatEvent battleEvent)
		{
			Debug.WriteLine("Battle event occurred.");
			if (battleEvent is MovementEvent me)
			{
				Debug.WriteLine($"{me.Source.Name} moved to ({me.NewPosition.Row},{me.NewPosition.Column})");
			}
			else if (battleEvent is DamageEvent de)
			{
				Debug.Write(de.Damage.ToStringRecursive());
			}
		}

		/// <summary>
		/// Class to help pull together input and keypresses with actions.
		/// </summary>
		/// <typeparam name="CONTEXT"></typeparam>
		/// <typeparam name="CALLBACKS"></typeparam>
		private class ActionHandler<CONTEXT, CALLBACKS>
		{
			List<Tuple<string, Action<CONTEXT, CALLBACKS>, Func<ConsoleKeyInfo, bool>>> keyActionList
				= new List<Tuple<string, Action<CONTEXT, CALLBACKS>, Func<ConsoleKeyInfo, bool>>>();


			private readonly char[] expectedInputOrder = "0123456789qwertyuiopasdfghjklzxcvbnm".ToArray();
			private int keyIndex = 0;

			public void AddAction(string name, Action<CONTEXT, CALLBACKS> action)
			{
				char keyChar = expectedInputOrder[keyIndex++];
				Func<ConsoleKeyInfo, bool> keyPredicate = (key => key.KeyChar == keyChar);
				keyActionList.Add(Tuple.Create($"{keyChar}. {name}", action, keyPredicate));
			}

			public void PrintActions() => keyActionList.ForEach(x => Console.WriteLine(x.Item1));

			public bool TryGetAction(ConsoleKeyInfo keyInfo, out Action<CONTEXT, CALLBACKS> action)
			{
				action = keyActionList.Where(x => x.Item3(keyInfo)).Select(x => x.Item2).FirstOrDefault();
				return action != null;
			}
		}
	}
}
