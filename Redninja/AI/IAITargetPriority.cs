using System.Collections.Generic;

namespace Redninja.AI
{
	public interface IAITargetPriority
	{				
		IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities);
	}
}