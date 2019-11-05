using System;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Buffs.Behaviors
{
	public class DamageOvertimeExecutionBehavior : IBuffExecutionBehavior
	{
		public DamageType DamageSource { get; private set; }
		public float TicksPerSecond { get; private set; }
		public float KRatePerSecond { get; private set; }

		// TODO put in damage scale to max HP or MP
		private float nextTick;
		
		public event Action<float, IBattleOperation> BattleOperationReady;

		public void Initialize(IBuff buff)
		{
			nextTick = TickDelta;
		}

		public void OnClockTick(float delta, IBuff buff)
		{			
			while(nextTick <= buff.CurrentDuration)
			{
				BattleOperationReady?.Invoke(buff.ExecutionStart + nextTick, new DamageOperation(buff.Owner, new StaticTarget(buff.TargetUnit), null));
				nextTick += TickDelta;
			}			
		}

		private float TickDelta => 1f / TicksPerSecond;

		private float DamagePerTick => KRatePerSecond / TicksPerSecond;
		
	}
}
