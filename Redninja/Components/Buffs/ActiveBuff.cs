using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Clock;

namespace Redninja.Components.Buffs
{
	public class ActiveBuff : IBuff, IClockSynchronized
	{
		public IClock Clock { get; private set; }
		
		private readonly IBuff buff;

		public float LastDuration { get; private set; }

		public float ExecutionStart { get; private set; } 

		public bool IsDurationBuff { get; private set; }

		public float CalculatedMaxDuration { get; private set; }		

		public IBattleContext Context { get; private set; }

		public IBattleEntity Source { get; private set; }

		public IBattleEntity Target { get; private set; }
		
		public bool IsExpired { get => CalculatedMaxDuration > 0 && LastDuration >= CalculatedMaxDuration; }

		public Dictionary<string, float> SavedState { get; } = new Dictionary<string, float>();

		public BuffConfig Config => buff.Config;

		public IBuffExecutionBehavior Behavior => buff.Behavior;

		public event Action OnBuffExpired;

		public ActiveBuff(IBuff buff)
		{
			this.buff = buff;
		}

		private void InitializeBuff(IBattleContext context, IBattleEntity source, IBattleEntity target)
		{
			this.Context = context;
			this.Source = source;
			this.Target = target;

			// TODO, apply any special properties about duration or other.
			LastDuration = Config.Duration;
			ExecutionStart = context.Clock.Time;
		}

		public void SetClock(IClock clock)
		{
			Dispose();
			this.Clock = clock;
			clock.Tick += OnClockTick;
		}

		public void Dispose()
		{
			if (Clock == null) return;
			Clock.Tick -= OnClockTick;
			Clock = null;
		}

		private void OnClockTick(float delta)
		{
			if (!IsExpired) Behavior?.OnClockTick(delta, this);
			LastDuration += delta;										
		}
	}
}
