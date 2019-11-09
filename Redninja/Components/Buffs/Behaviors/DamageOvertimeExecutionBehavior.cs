using System;
using Davfalcon;
using Davfalcon.Stats;
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
		private IStats tempStats;
		
		public event Action<float, IBattleOperation> BattleOperationReady;

		public void Initialize(IBuff buff)
		{
			nextTick = TickDelta;
			StatsMap tempStats = new StatsMap();
			tempStats[Stat.BleedDamageExtra] = (int)DamagePerTick;
			this.tempStats = tempStats;
		}

		public void OnClockTick(float delta, IBuff buff)
		{			
			while(nextTick <= buff.CurrentDuration)
			{
				BattleOperationReady?.Invoke(buff.ExecutionStart + nextTick, 
					new DamageTickOperation(buff.Owner, 
					new StaticTarget(buff.TargetUnit), /*buff.AsModified().Stats*/ tempStats, DamageSource));
				nextTick += TickDelta;
			}			
		}

		private float TickDelta => 1f / TicksPerSecond;

		private float DamagePerTick => KRatePerSecond / TicksPerSecond;
		
	}
}
