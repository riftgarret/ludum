using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public interface OperationResult
	{
		DamageSourceType SourceType { get; } 

		EventHistorian Historian { get; }

		DamageType DamageType { get; }

		int Total { get; }
	}
}
