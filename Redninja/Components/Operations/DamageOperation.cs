using System;
using Davfalcon.Revelator;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Operations
{
	public class DamageOperation : BattleOperationBase
	{
		private readonly IEntityModel unit;
		private readonly ITargetResolver target;
		private readonly IDamageSource source;

		public DamageOperation(IEntityModel unit, ITargetResolver target, IDamageSource source)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.source = source ?? throw new ArgumentNullException(nameof(source));
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IEntityModel t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.DealDamage(unit, t, source);
			}
		}
	}
}
