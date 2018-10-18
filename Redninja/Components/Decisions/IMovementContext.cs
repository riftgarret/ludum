namespace Redninja.Components.Decisions
{
	public interface IMovementContext : IMovementView, IActionProvider
	{
		void AddPoint(Coordinate point);
		bool Back();
	}
}
