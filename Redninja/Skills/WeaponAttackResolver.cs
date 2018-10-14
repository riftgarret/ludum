﻿using System;
using Davfalcon.Revelator;
using Redninja.Operations;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class WeaponAttackResolver : ISkillResolver
	{
		private readonly IWeapon weapon;
		private readonly ITargetResolver target;

		public bool Resolved { get; private set; } = false;
		public float ExecutionStart { get; }

		public IBattleOperation Resolve(IBattleEntity entity, ISkill skill)
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