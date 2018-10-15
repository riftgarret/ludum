using System.Collections.Generic;

namespace Redninja.Entities.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IUnitModel GetBestTarget(IEnumerable<IUnitModel> validEntities);
	}
}
