using System;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Entities;

namespace Redninja.Components.Actions
{
	[Serializable]
	public class UnitActionManager : IUnitActionManager
	{
		private IClock clock;

		protected IUnit Owner { get; private set; }

		public IBattleAction CurrentAction { get; protected set; }

		public string CurrentActionName => CurrentAction?.Name;

		public ActionPhase Phase => CurrentAction?.Phase ?? ActionPhase.Waiting;

		public float PhaseProgress => CurrentAction?.PhaseProgress ?? 0;

		public virtual bool RequiresAction => CurrentAction == null || CurrentAction.Phase == ActionPhase.Done;

		public event Action<IBattleEntity> ActionNeeded;
		public event Action<IBattleEntity, IOperationSource> ActionSet;

		public virtual void Initialize(IUnit unit)
		{
			Owner = unit;
		}

		public virtual void SetAction(IBattleAction action)
		{
			if (CurrentAction != null)
				CurrentAction.Dispose();

			CurrentAction = action;
			CurrentAction.SetClock(clock);  // TODO NRE on 2nd skill usage 

			// forgot what this is for, figure out if need and how to invoke
			//ActionSet?.Invoke(this, action);

			CurrentAction.Start();
		}

		protected virtual void OnActionCompleted()
		{
			// invoke after determining which interface to pass
			//ActionNeeded?.Invoke(this);

			// If we add an action queue, pop the completed action off here
		}

		private void OnTick(float timeDelta)
		{
			// This should probably be an event as well
			if (CurrentAction.Phase == ActionPhase.Done)
			{
				OnActionCompleted();
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
		}
	}
}
