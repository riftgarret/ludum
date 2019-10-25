using System;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Buffs.Behaviors
{
	public class DamageOvertimeExecutionBehavior : IBuffExecutionBehavior
	{
		public Stat DamageSource { get; private set; }
		public float TicksPerSecond { get; private set; }
		public float KRatePerSecond { get; private set; }

		// TODO put in damage scale to max HP or MP
		
		public event Action<float, IBattleOperation> BattleOperationReady;

		public void OnClockTick(float delta, ActiveBuff buff)
		{
			
			foreach (float tickTime in GetTicksFromLastUpdate(delta, buff, TicksPerSecond))
			{
				BattleOperationReady?.Invoke(tickTime, new DamageOperation(buff.Owner, new StaticTarget(buff.TargetUnit), null));
			}
		}

		private float DamagePerTick => KRatePerSecond / TicksPerSecond;

		private float[] GetTicksFromLastUpdate(float delta, ActiveBuff buff, float ticksPerSecond) 
		{
			float leftover = buff.LastDuration % ticksPerSecond;
			int ticks = (int) Math.Floor((leftover + delta) / ticksPerSecond);

			float[] tickTimes = new float[ticks];
			for (int i=0; i < ticks; i++)
			{
				tickTimes[i] = buff.ExecutionStart + buff.LastDuration + leftover + (1 / ticksPerSecond) * i;
			}
			return tickTimes;
		}
		
	}
}
