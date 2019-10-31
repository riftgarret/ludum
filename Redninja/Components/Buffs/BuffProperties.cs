using System;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public class BuffProperties
	{
		public bool IsGlobalCurable { get; set; }
		public bool IsDispellable { get; set; }
		public bool IsBleedCurable { get; set; }
		public Alignment Alignment { get; set; }
		public float Duration { get; set; }				
	}
}
