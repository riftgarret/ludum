using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Redninja.Logging;

namespace Redninja
{
	[TestFixture]
	public class TestBase
	{
		
		

		protected IBattleEntity CreateEntity(int curHp, int maxHp)  
		{
			LiveStatContainer container = new LiveStatContainer(maxHp);
			container.Current = curHp;
			IBattleEntity unit = Substitute.For<IBattleEntity>();
			unit.LiveStats[LiveStat.HP].Returns(container);
			return unit;
		}
	}
}
