using Davfalcon.Revelator;

namespace Redninja
{
	public interface IEntityModel
	{
		IUnit Character { get; }
		int Team { get; set; }
		EntityPosition Position { get; }

		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }

		void MovePosition(int row, int col);
	}
}
