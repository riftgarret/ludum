using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.Skills
{
	public interface IUnitSkillManager : ISkillProvider, IUnitComponent<IUnit>
	{
	}
}
