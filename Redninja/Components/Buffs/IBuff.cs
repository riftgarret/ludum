
using System;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	public interface IBuff : IBuff<IUnit>
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

		event Action<IBuff> BuffExpired;

		void InitializeBattleState(IBattleContext context, IBattleEntity owner, IBattleEntity target);
	}
}
