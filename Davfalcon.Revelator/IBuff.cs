namespace Davfalcon.Revelator
{
	public interface IBuff : ITimedModifier, IEffectSource
	{
		bool IsDebuff { get; }
		IUnit Owner { get; set; }
	}
}
