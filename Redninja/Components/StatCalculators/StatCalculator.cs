using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Redninja.Components.Combat;

namespace Redninja.Components.StatCalculators
{
	public abstract class StatCalculator<T> : StatCalculator
	{
		protected T Param { get; set; }

		public abstract int Calculate(IStats stats);

		public abstract void DamageOperationProcess(OperationContext oc);
	}

	public interface StatCalculator
	{
		int Calculate(IStats stats);

		void DamageOperationProcess(OperationContext oc);
	}
}
