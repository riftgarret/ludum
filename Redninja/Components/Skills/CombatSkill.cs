using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;
using TargetingSetFunc = Redninja.Components.Skills.SkillTargetingSet.Builder.Func;

namespace Redninja.Components.Skills
{
	public class CombatSkill : ISkill
	{
		public string Name { get; private set; }
		public ActionTime Time { get; private set; }

		public IReadOnlyList<SkillTargetingSet> Targets { get; private set; }

		public IBattleAction GetAction(IBattleEntity entity, List<ISelectedTarget> targets)
		{
			List<ISkillResolver> resolvers = new List<ISkillResolver>();
			for (int i = 0; i < Targets.Count; i++)
			{
				resolvers.AddRange(Targets[i].GetSkillResolvers(targets[i]));
			}
			return new SkillAction(entity, this, resolvers);
		}

		private CombatSkill() { }

		public class Builder : BuilderBase<CombatSkill, ISkill, Builder>
		{
			private readonly string name;
			private readonly ISkillOperationParameters defaultArgs;
			private List<SkillTargetingSet> targets;

			internal Builder(string name, ISkillOperationParameters defaultArgs)
			{
				this.name = name;
				this.defaultArgs = defaultArgs;
				Reset();
			}

			public override Builder Reset()
			{
				targets = new List<SkillTargetingSet>();
				CombatSkill skill = new CombatSkill()
				{
					Name = name,
					Targets = targets.AsReadOnly()
				};
				return Reset(skill);
			}

			public Builder SetActionTime(int prepare, int execute, int recover) => Self(s => s.Time = new ActionTime(prepare, execute, recover));
			public Builder SetActionTime(ActionTime actionTime) => Self(s => s.Time = actionTime);
			public Builder AddTargetingSet(TargetTeam team, TargetCondition condition, TargetingSetFunc builderFunc)
				=> AddTargetingSet(new TargetingRule(team, condition), builderFunc);
			public Builder AddTargetingSet(ITargetPattern targetPattern, TargetTeam team, TargetCondition condition, TargetingSetFunc builderFunc)
				=> AddTargetingSet(new TargetingRule(targetPattern, team, condition), builderFunc);
			public Builder AddTargetingSet(ITargetingRule targetingRule, TargetingSetFunc builderFunc)
				=> Self(s => targets.Add(builderFunc(new SkillTargetingSet.Builder(targetingRule, defaultArgs)).Build()));
		}

		// Schema loading
		public static ISkill Build(string name, ISkillOperationParameters defaultArgs, Builder.Func func) => func(new Builder(name, defaultArgs)).Build();		
	}
}
