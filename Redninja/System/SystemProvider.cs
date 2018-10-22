using Redninja.Data;

namespace Redninja.System
{
	internal class SystemProvider : ISystemProvider
	{
		private readonly IDataManager data;

		public SystemProvider(IDataManager data) => this.data = data;

		public IClassProvider GetClass(string className) => data.Classes[className];
	}
}
