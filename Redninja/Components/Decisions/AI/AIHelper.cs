using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	internal static class AIHelper
	{
		internal delegate int ExtractValue(IBattleEntity entity);	

		/// <summary>
		/// Filter total entities by AITargetType.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="source"></param>
		/// <param name="bem"></param>
		/// <returns></returns>
		internal static IEnumerable<IBattleEntity> FilterByType(TargetTeam type, IBattleEntity source, IBattleModel bem)
		{
			switch (type)
			{
				case TargetTeam.Ally:
					return bem.Entities.Where(x => x.Team == source.Team);
				case TargetTeam.Enemy:
					return bem.Entities.Where(x => x.Team != source.Team);
				case TargetTeam.Self:
					return Enumerable.Repeat(source, 1);					
				case TargetTeam.Any:
					return bem.Entities;
				default:
					throw new InvalidOperationException("Unexpected target type, should implement!");
			}
		}

		/// <summary>
		/// Helper method for finding best qualified member. 
		/// </summary>
		/// <param name="qualifier"></param>
		/// <param name="extractValueMethod"></param>
		/// <returns></returns>
		internal static IBattleEntity FindBestMatch(IEnumerable<IBattleEntity> entities, 
			AITargetingPriorityQualifier qualifier,  
			ExtractValue extractValueMethod)
		{
			if (entities.Count() == 0) throw new InvalidOperationException("Cannot find best match with no set");
			
			switch(qualifier)
			{
				case AITargetingPriorityQualifier.Average:
					double average = entities.Average(entity => extractValueMethod.Invoke(entity));
					return entities.OrderBy(entity => Math.Abs(extractValueMethod.Invoke(entity) - average))
						.First();

				case AITargetingPriorityQualifier.Highest:
					int maxValue = entities.Max(entity => extractValueMethod.Invoke(entity));
					return entities.FirstOrDefault(entity => maxValue == extractValueMethod.Invoke(entity));

				case AITargetingPriorityQualifier.Lowest:				
					int minValue = entities.Min(entity => extractValueMethod.Invoke(entity));
					return entities.FirstOrDefault(entity => minValue == extractValueMethod.Invoke(entity));

				case AITargetingPriorityQualifier.None:
				default:
					return entities.First();
			}			
		}
	}
}
