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
			dataManager.LoadJson(configFile);
			return dataManager;
		}
	}
}
