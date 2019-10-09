using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	// TODO remove
	public interface IActionProvider
	{
		IUnitModel Entity { get; }
		IBattleAction GetAction();
	}
}
