using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	public static class DataReader
	{
		public static void Read(IEditableDataManager manager, string configPath)
		{
			var config = ParseHelper.ReadJson<ConfigSchema>(configPath);

			SkillReader.ReadAll(ParseHelper.ReadJson<SkillRootSchema>(config.SkillsPath), manager);			
		}		
	}
}
