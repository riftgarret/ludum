using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IUnitModel GetBestTarget(IEnumerable<IUnitModel> validEntities);
	}
}
