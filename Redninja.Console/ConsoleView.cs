using System;
using System.Collections.Generic;
using System.Diagnostics;
using Davfalcon.Nodes;
using Redninja.Components.Decisions;
using Redninja.ConsoleDriver.Objects;
using Redninja.Events;
using Redninja.View;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		private IBattleModel model;
		private IUnitModel waiting;

		private Action drawActionNeeded;
		private Action drawUnitActions;
		private Action drawTargeting;

		public void Draw()
		{
			Console.Clear();
			foreach (IUnitModel entity in model.Entities)
			{
				entity.Print();
			}

			drawActionNeeded?.Invoke();
			drawUnitActions?.Invoke();
			drawTargeting?.Invoke();
		}

		public void SetBattleModel(IBattleModel model)
		{
			this.model = model;
		}

		public void OnDecisionNeeded(IUnitModel entity)
		{
			waiting = entity;
		}

		public void Resume()
		{
			waiting = null;
		}

		public void SetViewMode(IBaseCallbacks callbacks)
		{
			drawActionNeeded = () =>
			{
				if (waiting != null)
				{
					Console.WriteLine("Waiting for player input...");
					callbacks.SelectUnit(waiting);
				}
			};
			drawUnitActions = null;
			drawTargeting = null;
		}

		public void SetViewMode(IActionsView actionsContext, ISkillsCallbacks callbacks)
		{
			drawActionNeeded = null;
			drawUnitActions = () =>
			{
				IUnitModel entity = actionsContext.Entity;
				Console.WriteLine($"Select an action for {entity.Character.Name}");
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
			};
			drawTargeting = null;
		}

		public void SetViewMode(IMovementView movementContext, IMovementCallbacks callbacks)
		{
			throw new NotImplementedException();
		}

		public void SetViewMode(ITargetingView targetingContext, ITargetingCallbacks callbacks)
		{
			drawActionNeeded = null;
			drawUnitActions = null;
			drawTargeting = () =>
			{
				List<IUnitModel> availableTargets = new List<IUnitModel>();

				foreach (IUnitModel t in targetingContext.GetTargetableEntities())
				{
					Console.WriteLine(t.Character.Name);
					availableTargets.Add(t);
				}

				Console.WriteLine("Select target...");
				int selected = Convert.ToInt32(Console.ReadLine()) - 1;
				try
				{
					callbacks.SelectTarget(targetingContext.GetSelectedTarget(availableTargets[selected]));
				}
				catch (Exception)
				{
					callbacks.Cancel();
				}
			};
		}

		public void OnBattleEventOccurred(IBattleEvent battleEvent)
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
