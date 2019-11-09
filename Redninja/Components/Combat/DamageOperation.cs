using System;
using Davfalcon;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;

namespace Redninja.Components.Combat
{
	internal class DamageOperationDefinition : IBattleOperationDefinition, IWeaponSkillParam
	{
		public int SkillDamage { get; set; }
		public DamageType DamageType { get; set; }
		public WeaponSlotType SlotType { get; set; }
		public WeaponType WeaponType { get; set; }
		public IStats Stats { get; set; }		
		public float ExecutionStart { get; set; }

		public IBattleOperation CreateOperation(IBattleEntity source, ITargetResolver target)
			=> new DamageOperation(source, target, this);
	}

	internal class DamageOperation : IBattleOperation
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly DamageOperationDefinition def;

		internal DamageOperation(IBattleEntity unit, ITargetResolver target, DamageOperationDefinition def) 
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.def = def;
		}

		public float ExecutionStart => def.ExecutionStart;

		public void Execute(IBattleContext context)
		{
			IStats defStats = def.Stats;
			IStats weaponStats = OperationHelper.ExtractWeaponStats(def, unit);
			IStats combined = defStats.Join(weaponStats);

			foreach (IBattleEntity t in target.GetValidTargets(unit, context.BattleModel))
			{
				context.CombatExecutor.DealDamage(unit, t, combined, def.DamageType);
				RLog.D(this, $"Damage operation from: {unit.Name} to: {t.Name}");
			}
		}
	}
}
