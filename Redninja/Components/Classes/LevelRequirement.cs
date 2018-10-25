using System;
using Davfalcon.Revelator;

namespace Redninja.Components.Classes
{
	public class LevelRequirement : ICharacteristicRequirement
	{
		private int Level { get; }

		public LevelRequirement(int level) => this.Level = level;

		public bool IsAvailable(IUnit unit) => unit.Level >= Level;
	}
}
