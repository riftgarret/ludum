using Redninja.Components.Actions;
using Redninja.View;

namespace Redninja.Components.Actions.Decisions
{
	public interface IMovementComponent : IMovementState
	{
		void AddPoint(Coordinate point);
		bool Back();
		IBattleAction GetAction();
	}
}
