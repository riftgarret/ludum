
namespace Redninja.Components.Buffs
{
	public interface IBuff
	{
		BuffConfig Config { get; }
		IBuffExecutionBehavior Behavior { get; }
	}
}
