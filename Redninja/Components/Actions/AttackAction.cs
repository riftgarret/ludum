﻿using System;
using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Operations;
using Redninja.Components.Targeting;

namespace Redninja.Components.Actions
{
	public class AttackAction : BattleActionBase
	{
		private readonly IEntityModel unit;
		private readonly IEntityModel target;
		private readonly IWeapon weapon;
		private readonly List<float> times;

		public AttackAction(IEntityModel unit, IEntityModel target, IWeapon weapon, params float[] procTimes)
			: base(2, 5, 5)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
			times = new List<float>(procTimes);
			times.Sort();
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			while (times.Count > 0 && PhaseProgress >= times[0])
			{
				SendBattleOperation(GetPhaseTimeAt(times[0]), new DamageOperation(unit, new SelectedTarget(TargetingRule.Any, target), weapon));
				times.RemoveAt(0);
			}
		}
	}
}
