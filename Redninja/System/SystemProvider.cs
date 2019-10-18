using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Data;

namespace Redninja.System
{
	internal class SystemProvider : ISystemProvider
	{
		private readonly IDataManager data;
		private Dictionary<IBattleEntity, ISkillProvider> skillProviderMap = new Dictionary<IBattleEntity, ISkillProvider>();

		public SystemProvider(IDataManager data) => this.data = data;

		public IClassProvider GetClass(string className) => data.Classes[className];

		public ISkillProvider GetSkillProvider(IBattleEntity unit)
			=> skillProviderMap[unit];

		public void SetSkillProvider(IBattleEntity unit, ISkillProvider provider)
			=> skillProviderMap[unit] = provider;
	}
}
