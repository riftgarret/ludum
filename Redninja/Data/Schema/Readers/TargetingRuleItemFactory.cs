using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal class TargetingRuleItemFactory : IDataItemFactory<TargetingRule>
	{
		public TargetingRule CreateInstance(string dataId, ISchemaStore store)
		{
			TargetingRuleSchema item = store.GetSchema<TargetingRuleSchema>(dataId);
			TargetingRule rule;
			if (item.TargetType == TargetType.Pattern)
			{
				ITargetPattern pattern = ParseHelper.ParsePattern(item.Pattern);
				rule = new TargetingRule(pattern, item.TargetTeam, ParseHelper.ParseTargetCondition(item.TargetConditionName));
			}
			else
			{
				rule = new TargetingRule(item.TargetTeam, ParseHelper.ParseTargetCondition(item.TargetConditionName));
			}
			return rule;
		}
	}
}
