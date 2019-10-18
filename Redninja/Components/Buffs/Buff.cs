using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.Buffs
{
	public class Buff : IBuff
	{
		public BuffConfig Config { get; set; }
		public IBuffExecutionBehavior Behavior { get; set; }		
	}
}
