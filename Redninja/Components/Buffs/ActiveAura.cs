using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Buffs
{
	public class ActiveAura
	{
		public ActiveBuff MasterBuff { get; private set; }
		public BuffProperties BuffDefinition { get; private set; }
	}
}
