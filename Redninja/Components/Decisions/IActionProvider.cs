using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	// TODO remove
	public interface IActionProvider
	{
		IUnitModel Source { get; }
		IBattleAction GetAction();
	}
}
