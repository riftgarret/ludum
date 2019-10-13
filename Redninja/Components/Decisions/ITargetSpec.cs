using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public interface ITargetSpec
	{
		void Unselect();
		bool IsSelected { get; }
		ISelectedTarget SelectedTarget { get; }
		SkillTargetingSet TargetingSet { get; }
		TargetType TargetType { get; }
		ITargetingRule TargetRule { get; }
		ISkill Skill { get; }
	}
}
