using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja
{
	public interface IBattleViewCallbacks
	{
		void OnSkillSelected(IBattleEntity entity, ICombatSkill skill);
		void OnTargetSelected(IBattleEntity entity, ICombatSkill skill, SelectedTarget target);
	}
}
