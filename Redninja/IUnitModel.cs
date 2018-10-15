using Davfalcon.Revelator;

namespace Redninja
{
	public interface IUnitModel
	{
		IUnit Character { get; }
		int Team { get; set; }
		UnitPosition Position { get; }

		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }

		void MovePosition(int row, int col);
	}
}
