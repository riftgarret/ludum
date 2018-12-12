using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Skills;
using Redninja.Data;

namespace Redninja.System
{
	internal class SystemProvider : ISystemProvider
	{
		private readonly IDataManager data;
		private Dictionary<IUnitModel, ISkillProvider> skillProviderMap = new Dictionary<IUnitModel, ISkillProvider>();

		public SystemProvider(IDataManager data) => this.data = data;

		public IClassProvider GetClass(string className) => data.Classes[className];

		public ISkillProvider GetSkillProvider(IUnitModel unit)
			=> skillProviderMap[unit];

		public void SetSkillProvider(IUnitModel unit, ISkillProvider provider)
			=> skillProviderMap[unit] = provider;
	}
}
