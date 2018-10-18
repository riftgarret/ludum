using System;
using Davfalcon.Randomization;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;

namespace Redninja.Entities
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	internal class BattleEntity : IBattleEntity
	{
		private IClock clock;
		private readonly ICombatExecutor combatExecutor;

		public IUnit Character { get; }

		public int Team { get; set; }
		public UnitPosition Position { get; private set; } = new UnitPosition(1);

		// If we add an action queue here, this will point to the top instead
		public IBattleAction CurrentAction { get; private set; }
		string IUnitModel.CurrentActionName => CurrentAction?.Name;
		public ActionPhase Phase => CurrentAction?.Phase ?? ActionPhase.Waiting;
		public float PhaseProgress => CurrentAction?.PhaseProgress ?? 0;

		public IActionDecider ActionDecider { get; }

		public event Action<IBattleEntity, IBattleAction> ActionSet;
		public event Action<IBattleEntity> ActionNeeded;

		public BattleEntity(IUnit character, IActionDecider actionDecider, ICombatExecutor combatExecutor)
		{
			this.combatExecutor = combatExecutor;
			combatExecutor.EntityMoving += OnEntityMoving;

			Character = character;
			ActionDecider = actionDecider;
			ActionDecider.ActionSelected += OnActionSelected;
		}

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

		public void SetAction(IBattleAction action)
		{
			if (CurrentAction != null)
				CurrentAction.Dispose();

			CurrentAction = action;
			CurrentAction.SetClock(clock);
			ActionSet?.Invoke(this, action);
			CurrentAction.Start();
		}

		public void MovePosition(int row, int col)
			=> Position = new UnitPosition(row, col, Position.Size);

		private void OnTick(float timeDelta)
		{
			// Check for buff update interval, then update buffs/status effects

			if (CurrentAction.Phase == ActionPhase.Done)
			{
				ActionNeeded?.Invoke(this);
				// If we add an action queue, pop the completed action off here
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

			combatExecutor.EntityMoving -= OnEntityMoving;
			ActionDecider.ActionSelected -= OnActionSelected;
		}
	}
}
