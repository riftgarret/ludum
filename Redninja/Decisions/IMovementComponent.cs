namespace Redninja.Decisions
{
	public interface IMovementComponent : IMovementState
	{
		void AddPoint(Coordinate point);
		bool Back();
		IBattleAction GetAction();
	}
}
