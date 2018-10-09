using System;
using Redninja.Decisions;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		public void SetViewModeDefault()
		{
			throw new NotImplementedException();
		}

		public void SetViewModeTargeting(SkillTargetMeta targets)
		{
			throw new NotImplementedException();
		}

		public void UpdateEntity(IBattleEntity entity)
		{
			Console.WriteLine(entity.Print());
		}
	}
}
