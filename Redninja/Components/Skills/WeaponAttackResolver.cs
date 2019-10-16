﻿using System;
using Redninja.Components.Combat;
using Redninja.Components.Equipment;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	internal class WeaponAttackResolver : ISkillResolver
	{
		private readonly IWeapon weapon;
		private readonly ITargetResolver target;

		public float ExecutionStart { get; }
		public bool Resolved { get; private set; } = false;

		public IBattleOperation Resolve(IBattleEntity entity)
		{
			Resolved = true;
			return new DamageOperation(entity, target, weapon);
		}

		public WeaponAttackResolver(float executionStart, IWeapon weapon, ITargetResolver target)
		{
			this.weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
			this.target = target ?? throw new ArgumentNullException(nameof(target));

			ExecutionStart = executionStart;
		}
	}
}
