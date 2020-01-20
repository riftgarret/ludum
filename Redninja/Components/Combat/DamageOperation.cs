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

		public IBattleOperation CreateOperation(ISkill skill, IBattleEntity source, ITargetResolver target)
			=> new DamageOperation(skill, source, target, this);
	}

	internal class DamageOperation : IBattleOperation
	{
		private readonly ISkill parentSkill;
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly DamageOperationDefinition def;

		internal DamageOperation(ISkill parentSkill, IBattleEntity unit, ITargetResolver target, DamageOperationDefinition def) 
		{
			this.parentSkill = parentSkill ?? throw new ArgumentNullException(nameof(parentSkill)); ;
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
			IStatSource source = new SkillStatsSource(parentSkill, combined);

			foreach (IBattleEntity t in target.GetValidTargets(unit, context.BattleModel))
			{
				context.CombatExecutor.DealSkillDamage(unit, t, source, def.DamageType);
				RLog.D(this, $"Damage operation from: {unit.Name} to: {t.Name}");
			}
		}
	}
}
