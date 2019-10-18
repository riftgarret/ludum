
namespace Redninja.Components.Buffs
{
	public class Buff : IBuff
	{
		public BuffConfig Config { get; set; }
		public IBuffExecutionBehavior Behavior { get; set; }		
	}
}
