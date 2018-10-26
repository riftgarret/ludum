using System;
using Davfalcon.Revelator;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	internal class StatusEffectOperation : BattleOperationBase
	{
		private readonly IUnitModel unit;
		private readonly ITargetResolver target;
		private readonly IBuff statusEffect;

		public StatusEffectOperation(IUnitModel unit, ITargetResolver target, IBuff statusEffect)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.statusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
			statusEffect.Owner = unit;
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IUnitModel t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.ApplyStatusEffect(t, statusEffect);
			}
		}
	}
}
