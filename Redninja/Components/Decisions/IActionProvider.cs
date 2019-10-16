using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	// TODO remove
	public interface IActionProvider
	{
		IBattleEntity Source { get; }
		IBattleAction GetAction();
	}
}
