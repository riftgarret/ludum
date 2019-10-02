using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IDamageSource : INameable
	{
		int BaseDamage { get; }
		int CritMultiplier { get; }
		Enum BonusDamageStat { get; }
		IEnumerable<Enum> DamageTypes { get; }
	}
}
