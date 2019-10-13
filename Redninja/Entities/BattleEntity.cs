using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Randomization;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;
using Redninja.Components.Skills.StatusEffects;
using IUnit = Davfalcon.Revelator.IUnit;
using Redninja.Components.Properties;
using System.Linq;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Entities
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	internal class BattleEntity : IBattleEntity
	{
		private readonly IUnit unit;

		private IClock clock;
		private readonly ICombatExecutor combatExecutor;
		private string nameOverride = null;
		private IBattleContext context;

		private class StatusEffectManager : UnitModifierStack, IUnitModifierStack
		{
			private readonly BattleEntity entity;
			void IUnitModifierStack.Add(IUnitModifier item)
			{
				Add(item);
				if (item is IStatusEffect effect) entity.OnStatusEffectApplied(effect);
			}

			public StatusEffectManager(BattleEntity entity) => this.entity = entity;
		}

		#region Unit interface
		public string Name => nameOverride?? unit.Name;
		public string Class => unit.Class;
		public int Level => unit.Level;
		public IUnitEquipmentManager Equipment => unit.Equipment;

		// Rewire some properties so we can keep volatile elements out of the inner unit
		// Note that iterating over this Modifiers property will not iterate over Equipment like it normally would
		public IStats Stats => Modifiers.Stats;
		public IStatsPackage StatsDetails => Modifiers.StatsDetails;
		public IUnitModifierStack Modifiers => Buffs;
		public IUnitModifierStack Buffs { get; }
		public IDictionary<Enum, int> VolatileStats { get; } = new Dictionary<Enum, int>();
		#endregion

		// Maybe we can back these with VolatileStats
		public int Team { get; set; }
		public UnitPosition Position { get; private set; } = new UnitPosition(1);

		// If we add an action queue here, this will point to the top instead
		public IBattleAction CurrentAction { get; private set; }
		string IUnitModel.CurrentActionName => CurrentAction?.Name;
		public ActionPhase Phase => CurrentAction?.Phase ?? ActionPhase.Waiting;
		public float PhaseProgress => CurrentAction?.PhaseProgress ?? 0;
		
		public IActionContextProvider ActionContextProvider { get; }		

		public bool RequiresAction { get => AIBehavior == null && 
				(CurrentAction == null || CurrentAction.Phase == ActionPhase.Done); }

		// TODO pull properties from equipment, buffs, class def
		public IEnumerable<ITriggeredProperty> TriggeredProperties => Enumerable.Empty<ITriggeredProperty>();

		public IAIBehavior AIBehavior { get; private set; }

		public event Action<IBattleEntity> ActionNeeded;
		// Rename this
		public event Action<IBattleEntity, IOperationSource> ActionSet;		

		public BattleEntity(IBattleContext context, IUnit unit)
		{
			this.context = context;
			this.unit = unit;
			Buffs = new StatusEffectManager(this);
			Buffs.Bind(unit.Modifiers);

			this.combatExecutor = context.CombatExecutor;
			combatExecutor.EntityMoving += OnEntityMoving;

			ActionContextProvider = new ActionContextProvider(context, this);			
		}

		public void SetNameOverride(string nameOverride) => this.nameOverride = nameOverride;

		// Considering raising this stuff to BEM
		private void OnEntityMoving(IUnitModel entity, Coordinate c)
		{
			if (entity == this) MovePosition(c.Row, c.Column);
		}

		private void OnActionSelected(IUnitModel entity, IBattleAction action)
		{
			if (entity == this) SetAction(action);
		}

		public void InitializeBattlePhase()
		{
			combatExecutor.InitializeEntity(this);
			SetAction(new WaitAction(new RandomInteger(1, 10).Get()));
		}

		public void SetAIBehavior(AIRuleSet ruleSet)
		{
			if(ruleSet != null)
			{
				AIBehavior = new AIBehavior(context, this, ruleSet);				
			} else
			{
				AIBehavior = null;
			}
		}

		public void MovePosition(int row, int col)
			=> Position = new UnitPosition(row, col, Position.Size);

		public void SetAction(IBattleAction action)
		{
			if (CurrentAction != null)
				CurrentAction.Dispose();

			CurrentAction = action;
			CurrentAction.SetClock(clock);	// TODO NRE on 2nd skill usage 
			ActionSet?.Invoke(this, action);
			CurrentAction.Start();
		}

		private void OnStatusEffectApplied(IStatusEffect effect)
		{
			effect.SetClock(clock);
			effect.EffectTarget = this;
			effect.Expired += OnStatusEffectExpired;
			ActionSet?.Invoke(this, effect);
		}

		private void OnStatusEffectExpired(IStatusEffect effect)
		{
			effect.Dispose();
			combatExecutor.RemoveStatusEffect(this, effect);
		}

		private void OnTick(float timeDelta)
		{
			// This should probably be an event as well
			if (CurrentAction.Phase == ActionPhase.Done)
			{
				ActionNeeded?.Invoke(this);
				// If we add an action queue, pop the completed action off here

				if(AIBehavior != null)
				{
					var action = AIBehavior.DetermineAction();
					SetAction(action);
				}
			}
		}

		public void SetClock(IClock clock)
		{
			UnsetClock();

			this.clock = clock;
			clock.Tick += OnTick;
		}

		private void UnsetClock()
		{
			if (clock != null)
			{
				clock.Tick -= OnTick;
				clock = null;
			}
		}

		public void Dispose()
		{
			UnsetClock();

			if (CurrentAction != null)
			{
				CurrentAction.Dispose();
				CurrentAction = null;
			}

			foreach (IStatusEffect e in Buffs)
			{
				if (e != null) e.Dispose();
			}

			combatExecutor.CleanupEntity(this);
			combatExecutor.EntityMoving -= OnEntityMoving;
		}
	}
}
