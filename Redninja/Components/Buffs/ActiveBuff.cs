using System;
using System.Collections.Generic;
using Davfalcon.Buffs;
using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public sealed class ActiveBuff : Buff<IUnit>, IBuff, IUnit
	{
		private IBattleContext context;

		public BuffProperties Properties { get; set; }

		public IBuffExecutionBehavior Behavior { get; set; }

		public IBattleEntity Owner { get; private set; }

		public IBattleEntity TargetUnit { get; private set; }

		public float LastDuration { get; private set; }

		public float ExecutionStart { get; private set; }

		public bool IsDurationBuff { get; private set; }

		public float CalculatedMaxDuration { get; private set; }

		public bool IsExpired { get => CalculatedMaxDuration > 0 && LastDuration >= CalculatedMaxDuration; }

		// this can probably be private
		public Dictionary<string, float> SavedState { get; } = new Dictionary<string, float>();

		public event Action<IBuff> Expired;

		public event Action<float, IBattleOperation> BattleOperationReady;

		protected override IUnit SelfAsUnit => this;

		protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
			=> StatFunctions.Resolve(baseValue, modifications);

		protected override Func<int, int, int> GetAggregator(Enum modificationType)
			=> StatFunctions.GetAggregator(modificationType);

		protected override int GetAggregatorSeed(Enum modificationType)
			=> StatFunctions.GetAggregatorSeed(modificationType);

		public void InitializeBattleState(IBattleContext context, IBattleEntity owner, IBattleEntity target)
		{
			this.context = context;
			this.context.Clock.Tick += OnTick;

			Owner = owner;
			TargetUnit = target;

			// TODO Add self to target unit modifiers? Or rely on external set?

			// TODO, apply any special properties about duration or other.
			LastDuration = Properties.Duration;
			ExecutionStart = context.Clock.Time;
		}

		private void OnTick(float timeDelta)
		{
			Behavior.OnClockTick(timeDelta, this);

			// track duration here

			if (IsExpired)
			{
				Expired?.Invoke(this);
			}
		}

		private void UnsetClock()
		{
			if (context != null)
			{
				context.Clock.Tick -= OnTick;
			}
		}

		public void Dispose()
		{
			UnsetClock();
		}
	}
}
