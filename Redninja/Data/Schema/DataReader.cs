﻿namespace Redninja.Data.Schema
{
	internal static class DataReader
	{
		public static void Read(IEditableDataManager manager, string configPath)
		{
			var config = ParseHelper.ReadJson<ConfigSchema>(configPath);

			SkillReader.ReadAll(ParseHelper.ReadJson<SkillRootSchema>(config.SkillsPath), manager);			
		}		
	}
}
