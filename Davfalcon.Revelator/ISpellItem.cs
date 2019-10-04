namespace Davfalcon.Revelator
{
	public interface ISpellItem : IUsableItem
	{
		ISpell Spell { get; }
	}
}
