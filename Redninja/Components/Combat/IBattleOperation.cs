namespace Redninja.Components.Combat
{
	public interface IBattleOperation
	{		
		float ExecutionStart { get; }
		void Execute(IBattleContext context);
	}
}
