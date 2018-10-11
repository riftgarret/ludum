using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	internal class AIHelper
	{
		internal delegate int ExtractValue(IBattleEntity entity);

		private AIHelper()
		{

		}


		/// <summary>
		/// Filter total entities by AITargetType.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="source"></param>
		/// <param name="bem"></param>
		/// <returns></returns>
		internal static IEnumerable<IBattleEntity> FilterByType(AITargetType type, IBattleEntity source, IBattleEntityManager bem)
		{
			switch (type)
			{
				case AITargetType.Ally:
					return bem.AllEntities.Where(x => x.Team == source.Team);
				case AITargetType.Enemy:
					return bem.AllEntities.Where(x => x.Team != source.Team);
				case AITargetType.Self:
					return new List<IBattleEntity>() { source };
				default:
					throw new InvalidProgramException("Unexpected target type, should implement!");
			}
		}

		/// <summary>
		/// Helper to apply the proper conditional to evaluate the condition.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="op"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		internal static bool EvaluateCondition(double left, AIValueConditionOperator op, double right)
		{
			switch(op)
			{				
				case AIValueConditionOperator.LT:
					return left < right;
				case AIValueConditionOperator.GT:
					return left > right;
				case AIValueConditionOperator.EQ:
				default:
					return left == right;
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
