using System;
using Davfalcon;
using Davfalcon.Stats;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;

namespace Redninja.Components.Combat
{	
	internal class DamageTickOperation : IBattleOperation
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly IStats opStats;
		private readonly DamageType damageType;

		internal DamageTickOperation(IBattleEntity unit, ITargetResolver target, IStats opStats, DamageType damageType) 
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.opStats = opStats;
			this.damageType = damageType;
		}

		public float ExecutionStart => 0;

		private class TempStatSource : IStatSource
		{
			public string Name => "Temp";

			public IStats Stats { get; set; }
		}

		public void Execute(IBattleContext context)
		{
			TempStatSource skillSource = new TempStatSource();
			skillSource.Stats = opStats;
			foreach (IBattleEntity t in target.GetValidTargets(unit, context.BattleModel))
			{
				context.CombatExecutor.DealTickDamage(unit, t, skillSource, damageType);
				RLog.D(this, $"Damage Tick operation from: {unit.Name} to: {t.Name}");
			}
		}
	}
}
