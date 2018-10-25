using System;

namespace Redninja.Components.Operations
{
	public interface IOperationGenerator
	{
		event Action<float, IBattleOperation> BattleOperationReady;
	}
}
