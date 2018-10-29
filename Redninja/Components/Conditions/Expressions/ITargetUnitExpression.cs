using System;
using System.Collections.Generic;

namespace Redninja.Components.Conditions.Expressions
{
	public interface ITargetUnitExpression: IInitialExpression, IChainableExpression
	{
		IEnumerable<IUnitModel> Result(IUnitModel self, IUnitModel target, IBattleModel battleModel);
	}
}
