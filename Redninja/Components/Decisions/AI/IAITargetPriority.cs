using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities);
	}
}
