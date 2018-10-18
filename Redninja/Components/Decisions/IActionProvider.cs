using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	public interface IActionProvider
	{
		IUnitModel Entity { get; }
		IBattleAction GetAction();
	}
}
