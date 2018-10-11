using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Davfalcon.Nodes;
using Redninja.Actions;
using Redninja.ConsoleDriver.Objects;
using Redninja.Decisions;
using Redninja.Events;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		private IBattleModel model;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;
		public event Action<IBattleEntity, ICombatSkill> SkillSelected;
		public event Action<ISelectedTarget> TargetSelected;
		public event Action TargetingCanceled;

		public void Draw()
		{
			Console.Clear();
			foreach (IBattleEntity entity in model.AllEntities)
			{
				entity.Print();
			}
		}

		public void BattleEventOccurred(IBattleEvent battleEvent)
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

		public void NotifyDecisionNeeded(IBattleEntity entity)
		{
			Draw();
			Console.WriteLine("Waiting for player input...");
			ConsoleKey key = Console.ReadKey().Key;

			IBattleAction action = null;

			switch (key)
			{
				case ConsoleKey.A:
					if (model.EnemyEntities.Count() > 0)
						action = new AttackAction(entity, model.EnemyEntities.First(), Weapons.Sword, 0.25f, 0.5f, 0.75f);
					break;
				case ConsoleKey.M:
					action = new MovementAction(entity, entity.Position.Row + 1, entity.Position.Column + 1);
					break;
				case ConsoleKey.S:
					SkillSelected?.Invoke(entity, CombatSkills.DemoTargetedSkill);
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

		public void SetViewModeTargeting(ISkillTargetingInfo targetingInfo)
		{
			Draw();
			List<IBattleEntity> availableTargets = new List<IBattleEntity>();

			foreach (IBattleEntity t in targetingInfo.GetTargetableEntities())
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
			catch (Exception ex)
			{
				TargetingCanceled?.Invoke();
			}
		}
	}
}
