using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class UsableItem : IUsableItem, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Owner { get; set; }

		public bool IsConsumable { get; set; }
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		public void Use()
		{
			if (IsConsumable) Remaining--;
		}
	}
}
