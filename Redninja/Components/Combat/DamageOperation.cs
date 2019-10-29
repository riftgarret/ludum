using System;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	internal class DamageOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly ISkillOperationParameters paramz;

		public DamageOperation(IBattleEntity unit, ITargetResolver target, ISkillOperationParameters paramz)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.paramz = paramz ?? throw new ArgumentNullException(nameof(paramz));
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IBattleEntity t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.DealDamage(unit, t, paramz);
			}
		}
	}
}
