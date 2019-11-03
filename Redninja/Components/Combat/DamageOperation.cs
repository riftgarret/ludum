using System;
using Davfalcon;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	internal class DamageOperationDefinition : IBattleOperationDefinition
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

	internal class DamageOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly DamageOperationDefinition def;

		internal DamageOperation(IBattleEntity unit, ITargetResolver target, DamageOperationDefinition def) : base(def.ExecutionStart)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.def = def;
		}

		protected override void OnExecute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IBattleEntity t in target.GetValidTargets(unit, battleModel))
			{
				// TODO, implement damage logic here
			}			
		}
	}
}
