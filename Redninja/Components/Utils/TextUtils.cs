using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Utils
{
	public static class TextUtils
	{
		public static List<string> CreateUniqueNames(List<string> names)
		{
			Dictionary<string, int> nameCountMap = new Dictionary<string, int>();
			Dictionary<string, char> uniqueCharMap = new Dictionary<string, char>();
			foreach(string s in names)
			{
				nameCountMap[s] = nameCountMap.ContainsKey(s) ? (nameCountMap[s] + 1) : 1;
				uniqueCharMap[s] = 'a';
			}

			List<string> results = new List<string>();
			foreach(string s in names)
			{
				results.Add(nameCountMap[s] == 1 ? s : $"{s} {uniqueCharMap[s]++}");				
			}
			return results;
		}
	}
}
