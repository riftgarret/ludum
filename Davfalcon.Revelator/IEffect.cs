namespace Davfalcon.Revelator
{
	public interface IEffect
	{
		void Resolve(IUnit unit, IUnit target, params object[] args);
	}
}
