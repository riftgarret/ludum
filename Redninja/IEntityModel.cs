using Davfalcon.Revelator;

namespace Redninja
{
	public interface IEntityModel
	{
		IUnit Character { get; }
		int Team { get; set; }
		EntityPosition Position { get; }

		void MovePosition(int row, int col);
	}
}
