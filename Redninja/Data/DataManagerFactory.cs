using System.Collections.Generic;
using System.IO;
using Redninja.Data.Schema;
using Redninja.Data.Schema.Readers;

namespace Redninja.Data
{
	/// <summary>
	/// Create a data manager. Use case to hide internal implementation of DataManager.
	/// </summary>
	public static class DataManagerFactory
	{
		public static IDataManager Create(string configFile)
		{
			DataManager dataManager = new DataManager();

			var configs = ParseHelper.ReadJson<List<string>>(configFile);

			configs.ForEach(x => dataManager.LoadJson(File.ReadAllText(x)));

			return dataManager;
		}
	}
}
