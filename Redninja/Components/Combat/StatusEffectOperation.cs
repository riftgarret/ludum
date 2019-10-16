using System;
using Redninja.Components.Buffs;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	internal class StatusEffectOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly IBuff statusEffect;

		public StatusEffectOperation(IBattleEntity unit, ITargetResolver target, IBuff statusEffect)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.statusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
			statusEffect.Owner = unit;
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IBattleEntity t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.ApplyStatusEffect(unit, t, statusEffect);
			}
		}
	}
}
