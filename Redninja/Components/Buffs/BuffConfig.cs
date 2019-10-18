using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Buffs
{
	public class BuffConfig
	{
		public bool IsGlobalCurable { get; private set; }
		public bool IsDispellable { get; private set; }
		public bool IsBleedCurable { get; private set; }
		public Alignment Alignment { get; private set; }
		public float Duration { get; private set; }				
	}
}
