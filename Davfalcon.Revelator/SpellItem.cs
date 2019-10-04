using System;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class SpellItem : UsableItem, ISpellItem
	{
		public ISpell Spell { get; set; }
	}
}
