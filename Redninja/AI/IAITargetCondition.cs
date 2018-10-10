using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	public interface IAITargetCondition
	{
		bool IsValid(IBattleEntity entity);
	}
}
