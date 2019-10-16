using System;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;

namespace Redninja.Components.Actions
{
	[Serializable]
	public class UnitActionManager : IUnitActionManager
	{
		protected IBattleContext BattleContext { get; }

		protected IBattleEntity BattleEntity { get; }

		public IBattleAction CurrentAction { get; protected set; }

		public string CurrentActionName => CurrentAction?.Name;

		public ActionPhase Phase => CurrentAction?.Phase ?? ActionPhase.Waiting;

		public float PhaseProgress => CurrentAction?.PhaseProgress ?? 0;

		public virtual bool RequiresAction => CurrentAction == null || CurrentAction.Phase == ActionPhase.Done;

		public IActionContextProvider ActionContextProvider { get; }

		public event Action<IBattleEntity> ActionNeeded;
		public event Action<IBattleEntity, IOperationSource> ActionSet;

		public UnitActionManager(IBattleContext context, IBattleEntity entity)
		{
			BattleContext = context;
			BattleEntity = entity;

			BattleContext.Clock.Tick += OnTick;

			ActionContextProvider = new ActionContextProvider(context, entity);
		}

		public virtual void Initialize(IUnit unit)
		{
		}

		public virtual void SetAction(IBattleAction action)
		{
			if (CurrentAction != null)
				CurrentAction.Dispose();

			CurrentAction = action;
			CurrentAction.SetClock(BattleContext.Clock);  // TODO NRE on 2nd skill usage 

			// forgot what this is for, figure out if needed
			ActionSet?.Invoke(BattleEntity, action);

			CurrentAction.Start();
		}

		protected virtual void OnActionCompleted()
		{
			ActionNeeded?.Invoke(BattleEntity);

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

		private void UnsetClock()
		{
			if (BattleContext != null)
			{
				BattleContext.Clock.Tick -= OnTick;
			}
		}

		public void Dispose()
		{
			if (CurrentAction != null)
			{
				CurrentAction.Dispose();
				CurrentAction = null;
			}
		}
	}
}
