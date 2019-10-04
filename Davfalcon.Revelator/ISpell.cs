using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface ISpell : IDamageSource, IEffectSource, IDescribable
	{
		Enum TargetType { get; }
		int Cost { get; }
		int BaseHeal { get; }
		int Range { get; }
		int Size { get; }
		int MaxTargets { get; }
		IEnumerable<IBuff> GrantedBuffs { get; }
	}
}
