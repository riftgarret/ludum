using Redninja.Skills;
using System;

namespace Redninja.Actions
{
	/// <summary>
	/// Base implementation that handles incrementing time. It is required by the 
	/// subclass to call SetPhase() as part of its initialization.
	/// </summary>
	public abstract class BattleActionBase : IBattleAction
	{
		private IClock clock;
		private float phaseStart;
		private float phaseComplete;

		public PhaseState Phase { get; private set; }
		public float PhaseTime => phaseComplete - phaseStart;
		public float PhaseProgress => PhaseTime == 0 ? 1f : Math.Min((clock.Time - phaseStart) / PhaseTime, 1f);

		public ActionTime ActionTime { get; }
		public float TimePrepare => ActionTime.Prepare;
		public float TimeExecute => ActionTime.Execute;
		public float TimeRecover => ActionTime.Recover;

		public event Action<IBattleAction> ActionExecuting;
		public event Action<IBattleOperation> BattleOperationReady;

		protected abstract void ExecuteAction(float timeDelta, float time);

		protected void SendBattleOperation(IBattleOperation operation)
		{
			BattleOperationReady?.Invoke(operation);
		}
		protected BattleActionBase(ActionTime actionTime)
		{
			ActionTime = actionTime;
		}

		protected BattleActionBase(float prepare, float execute, float recover)
			: this(new ActionTime(prepare, execute, recover)) { }

		protected void SetPhase(PhaseState newPhase)
		{
			Phase = newPhase;

			// In case of manual/premature phase changes, set start time to current time
			// Otherwise, set it to intended completion time to account for clock overshooting
			phaseStart = Math.Min(clock.Time, phaseComplete);

			switch (newPhase)
			{
				case PhaseState.Preparing:
					if (TimePrepare <= 0) IncrementPhase();
					else phaseComplete = phaseStart + TimePrepare;
					break;
				case PhaseState.Executing:
					if (TimeExecute <= 0) IncrementPhase();
					else
					{
						phaseComplete = phaseStart + TimeExecute;
						ActionExecuting?.Invoke(this);
					}
					break;
				case PhaseState.Recovering:
					if (TimeRecover <= 0) IncrementPhase();
					else phaseComplete = phaseStart + TimeRecover;
					break;
				case PhaseState.Done:
					Dispose();
					break;
			}
		}

		private void IncrementPhase()
		{
			if (Phase < PhaseState.Done)
			{
				SetPhase(Phase + 1);
			}
		}

		private void OnTick(float timeDelta)
		{
			if (Phase == PhaseState.Done)
			{
				return;
			}

			if (Phase == PhaseState.Executing)
			{
				ExecuteAction(timeDelta, clock.Time);
			}

			if (clock.Time >= phaseComplete)
			{
				IncrementPhase();
			}
		}

		public void Start()
		{
			phaseComplete = clock.Time;
			SetPhase(PhaseState.Preparing);
		}

		#region Clock binding
		public void Start(IClock clock)
		{
			SetClock(clock);
			Start();
		}

		public void SetClock(IClock clock)
		{
			// Check to unbind from previous clock just in case
			Dispose();

			this.clock = clock;
			clock.Tick += OnTick;
		}

		public void Dispose()
		{
			if (clock != null)
			{
				clock.Tick -= OnTick;
				clock = null;
			}
		}
		#endregion

		public override string ToString()
			=> $"{GetType()}: {Phase} {(int)(PhaseProgress * 100)}%";
	}
}
