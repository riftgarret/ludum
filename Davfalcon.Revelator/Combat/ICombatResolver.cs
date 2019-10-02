using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	public interface ICombatResolver
	{
		ICombatOperations Operations { get; }

		void Initialize(IUnit unit);
		void Upkeep(IUnit unit);
		void Cleanup(IUnit unit);
		int AdjustVolatileStat(IUnit unit, Enum stat, int change);
		void ApplyBuff(IUnit unit, IBuff buff, IUnit source = null);
		void RemoveBuff(IUnit unit, IBuff buff);
		void ApplyEffects(IEffectSource source, IUnit owner, IUnit target);
		HitCheck CheckForHit(IUnit unit, IUnit target);
		IEnumerable<Enum> GetDamageScalingStats(IDamageSource source);
		IEnumerable<Enum> GetDamageScalingStats(IEnumerable<Enum> damageTypes);
		IEnumerable<Enum> GetDamageDefendingStats(IDamageSource source);
		IEnumerable<Enum> GetDamageDefendingStats(IEnumerable<Enum> damageTypes);
		IDamageNode GetDamageNode(IUnit unit, IDamageSource source);
		IDefenseNode GetDefenseNode(IUnit defender, IDamageNode damage);
		IEnumerable<StatChange> ApplyDamage(IDefenseNode damage);
	}
}
