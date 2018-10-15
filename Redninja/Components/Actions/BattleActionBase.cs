using System;
using Redninja.Components.Operations;

namespace Redninja.Components.Actions
{
	/// <summary>
	/// Base implementation that handles incrementing time. It is required by the 
	/// subclass to call SetPhase() as part of its initialization.
	/// </summary>
	public abstract class BattleActionBase : IBattleAction
	{
		private IClock clock;
		private float phaseComplete;

		public ActionPhase Phase { get; private set; }
		public float PhaseStart { get; private set; }
		public float PhaseTime => phaseComplete - PhaseStart;
		public float PhaseProgress => PhaseTime == 0 ? 1f : Math.Min((clock.Time - PhaseStart) / PhaseTime, 1f);

		public ActionTime ActionTime { get; }
		public float TimePrepare => ActionTime.Prepare;
		public float TimeExecute => ActionTime.Execute;
		public float TimeRecover => ActionTime.Recover;

		public event Action<IBattleAction> ActionExecuting;
		public event Action<float, IBattleOperation> BattleOperationReady;

		protected abstract void ExecuteAction(float timeDelta, float time);

		protected void SendBattleOperation(float time, IBattleOperation operation)
		{
			BattleOperationReady?.Invoke(time, operation);
		}
		protected BattleActionBase(ActionTime actionTime)
		{
			ActionTime = actionTime;
		}

		protected BattleActionBase(float prepare, float execute, float recover)
			: this(new ActionTime(prepare, execute, recover)) { }

		protected float GetPhaseTimeAt(float percent)
			=> PhaseStart + PhaseTime * percent;

		protected void SetPhase(ActionPhase newPhase)
		{
			// In case of manual/premature phase changes, set start time to current time
			// Otherwise, set it to intended completion time to account for clock overshooting
			PhaseStart = Math.Min(clock.Time, phaseComplete);

			bool done = false;
			while (!done)
			{
				Phase = newPhase;
				float phaseTime = 0;
				switch (newPhase)
				{
					case ActionPhase.Preparing:
						phaseTime = TimePrepare;
						break;
					case ActionPhase.Executing:
						phaseTime = TimeExecute;
						break;
					case ActionPhase.Recovering:
						phaseTime = TimeRecover;
						break;
					case ActionPhase.Done:
						Dispose();
						return;
				}

				if (phaseTime <= 0)
				{
					newPhase++;
				}
				else
				{
					done = true;
					phaseComplete = PhaseStart + phaseTime;
					if (Phase == ActionPhase.Executing)
					{
						ActionExecuting?.Invoke(this);
					}
				}
			}
		}

		private void IncrementPhase()
		{
			if (Phase < ActionPhase.Done)
			{
				SetPhase(Phase + 1);
			}
		}

		private void OnTick(float timeDelta)
		{
			if (Phase == ActionPhase.Done)
			{
				return;
			}

			if (Phase == ActionPhase.Executing)
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
			SetPhase(ActionPhase.Preparing);
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
