using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEffectSource : INameable
	{
		IEnumerable<IEffect> Effects { get; }
	}
}
