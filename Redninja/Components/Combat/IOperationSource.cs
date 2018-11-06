using System;

namespace Redninja.Components.Combat
{
	public interface IOperationSource
	{
		event Action<float, IBattleOperation> BattleOperationReady;
	}
}
