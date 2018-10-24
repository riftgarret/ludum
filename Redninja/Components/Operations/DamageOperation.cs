using System;
using Davfalcon.Revelator;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Operations
{
	internal class DamageOperation : BattleOperationBase
	{
		private readonly IUnitModel unit;
		private readonly ITargetResolver target;
		private readonly IDamageSource source;

		public DamageOperation(IUnitModel unit, ITargetResolver target, IDamageSource source)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.source = source ?? throw new ArgumentNullException(nameof(source));
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IUnitModel t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.DealDamage(unit, t, source);
			}
		}
	}
}
