using System;
using System.Collections.Generic;
using System.Diagnostics;
using Davfalcon.Nodes;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.ConsoleDriver.Objects;
using Redninja.Entities;
using Redninja.Events;
using Redninja.View;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		private IBattleModel model;

		public event Action<IEntityModel, IBattleAction> ActionSelected;
		public event Action<IEntityModel, ISkill> SkillSelected;
		public event Action<ISelectedTarget> TargetSelected;
		public event Action TargetingCanceled;
		public event Action<IEntityModel> MovementInitiated;
		public event Action<Coordinate> MovementPathUpdated;
		public event Action MovementConfirmed;

		public void Draw()
		{
			Console.Clear();
			foreach (IEntityModel entity in model.Entities)
			{
				entity.Print();
			}
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

		public void OnDecisionNeeded(IEntityModel entity)
		{
			Draw();
			Console.WriteLine("Waiting for player input...");
			ConsoleKey key = Console.ReadKey().Key;

			IBattleAction action = null;

			switch (key)
			{
				case ConsoleKey.A:
					SkillSelected?.Invoke(entity, CombatSkills.TwoHandedAttack);
					break;
				case ConsoleKey.M:
					action = new MovementAction(entity, entity.Position.Row + 1, entity.Position.Column + 1);
					break;
				case ConsoleKey.S:
					SkillSelected?.Invoke(entity, CombatSkills.PatternSkill);
					break;
				default:
					action = new WaitAction(5);
					break;
			}

			if (action != null) ActionSelected?.Invoke(entity, action);
		}

		public void SetBattleModel(IBattleModel model)
		{
			this.model = model;
		}

		public void SetViewModeDefault()
		{
			Draw();
		}

		public void SetViewMode(ITargetingView targetingInfo)
		{
			Draw();
			List<IEntityModel> availableTargets = new List<IEntityModel>();

			foreach (IEntityModel t in targetingInfo.GetTargetableEntities())
			{
				Console.WriteLine(t.Character.Name);
				availableTargets.Add(t);
			}

			Console.WriteLine("Select target...");
			int selected = Convert.ToInt32(Console.ReadLine()) - 1;
			try
			{
				TargetSelected?.Invoke(targetingInfo.GetSelectedTarget(availableTargets[selected]));
			}
			catch (Exception)
			{
				TargetingCanceled?.Invoke();
			}
		}

		public void SetViewMode(IMovementView movementState)
		{
			throw new NotImplementedException();
		}
	}
}
