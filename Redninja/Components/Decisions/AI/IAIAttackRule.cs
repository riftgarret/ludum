using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Decisions.AI
{
	public interface IAIAttackRule
	{
		IAITargetPriority TargetPriority { get; }
	}
}
