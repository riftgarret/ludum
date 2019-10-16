﻿using System;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	internal class DamageOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly IDamageSource source;

		public DamageOperation(IBattleEntity unit, ITargetResolver target, IDamageSource source)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.source = source ?? throw new ArgumentNullException(nameof(source));
		}

		public override void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			foreach (IBattleEntity t in target.GetValidTargets(unit, battleModel))
			{
				combatExecutor.DealDamage(unit, t, source);
			}
		}
	}
}
