using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Combat;
using Redninja.Logging;

namespace Redninja.Components.Actions
{
	internal class BattleOperationAction : BattleActionBase
	{		
		private readonly IEnumerable<OperationTracker> operations;

		public BattleOperationAction(string name, ActionTime time, IEnumerable<IBattleOperation> operations)
			: base(name, time)
		{
			this.operations = operations.Select(op => new OperationTracker(op)).ToList();			
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			foreach (OperationTracker op in operations)
			{
				if (!op.IsTriggered && PhaseProgress >= op.ExecutionStart)
				{
					RLog.D(this, $"Executing Operation {op}");
					SendBattleOperation(GetPhaseTimeAt(op.ExecutionStart), op.Trigger());					
				}
			}
		}


		private class OperationTracker
		{			
			/// <summary>
			/// Triggered is to be added to the queue.
			/// </summary>
			public bool IsTriggered { get; private set; }
			/// <summary>
			/// Executed means after processed in the queue.
			/// </summary>
			public bool IsExecuted { get; private set; }		

			public IBattleOperation Trigger()
			{
				IsTriggered = true;
				return operation;
			}

			public float ExecutionStart => operation.ExecutionStart;			

			private IBattleOperation operation;

			public OperationTracker(IBattleOperation operation)
			{
				this.operation = operation;
			}

			public override string ToString()
			{
				return operation.ToString();
			}
		}
	}
}
