using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	abstract class StatCalculator<T>
	{
		protected abstract T Param { get; }

		public virtual int Calculate(IStats stats) => CalculateExtra(Param, stats, CalculateCommon(Param, stats));

		protected abstract int CalculateCommon(T param, IStats stats);

		protected virtual int CalculateExtra(T param, IStats stats, int commonValue) { return commonValue; }
	}
}
