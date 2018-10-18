namespace Redninja.View
{
	public interface IMovementCallbacks
	{
		void UpdatePath(Coordinate point);
		void Confirm();
		void Cancel();
	}
}
