using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Decisions.AI
{
	public interface IAITargetCondition
	{
		bool IsValid(IBattleEntity entity);
	}
}
