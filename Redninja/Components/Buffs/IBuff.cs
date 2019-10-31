
using System;
using Davfalcon.Buffs;
using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	public interface IBuff : IBuff<IUnit>, IOperationSource, IDisposable
	{
		BuffProperties Properties { get; }
		IBuffExecutionBehavior Behavior { get; }
		IBattleEntity Owner { get; }
		IBattleEntity TargetUnit { get; }

		float LastDuration { get; }
		float ExecutionStart { get; }
		bool IsDurationBuff { get; }
		float CalculatedMaxDuration { get; }
		bool IsExpired { get; }

		event Action<IBuff> Expired;

		void InitializeBattleState(IBattleContext context, IBattleEntity owner, IBattleEntity target);
	}
}
