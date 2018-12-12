using System.Collections.Generic;
using Redninja.Components.Conditions;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface IStateCondition
	{
		IEnumerable<ICondition> Conditions { get; }
		void IsValid(IUnitModel self, IBattleModel battleModel);
	}
}
