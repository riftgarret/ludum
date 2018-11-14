using Davfalcon.Revelator;

namespace Redninja
{
	public interface IUnitModel : IUnit
	{
		int Team { get; set; }
		UnitPosition Position { get; }

		string CurrentActionName { get; }
		ActionPhase Phase { get; }
		float PhaseProgress { get; }
	}
}
