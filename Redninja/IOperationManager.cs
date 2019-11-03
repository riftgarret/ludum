using Redninja.Components.Combat;
using Redninja.Presenter;

namespace Redninja
{
	public interface IOperationManager
	{
		void Enqueue(float atTime, IBattleOperation operation);
	}

	internal class OperationManager : PriorityProcessingQueue<float, IBattleOperation>, IOperationManager
	{
		public OperationManager(IBattleContext context) 
			: base(op => op.Execute(context))
		{
		}
	}
}
