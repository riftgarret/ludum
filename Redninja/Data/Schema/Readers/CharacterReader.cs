using System.Collections.Generic;

namespace Redninja.Data.Schema.Readers
{
	internal static class CharacterReader
	{		
		public static void ReadRoot(List<CharacterSchema> characters, IEditableDataManager manager)
		{
			foreach(CharacterSchema cs in characters)
			{
				var unit = new Unit();
				foreach (var item in cs.Stats)
				{
					unit.BaseStats[item.Key] = item.Value;
				}
				//IUnit unit = Unit.Build(b =>
				//{
				//	b.SetMainDetails(cs.Name, cs.Class, cs.Level);

				//	foreach (var item in cs.Stats)
				//	{
				//		b.SetBaseStat(item.Key, item.Value);
				//	}

				//	return b;
				//});

				manager.NPCUnits[cs.DataId] = unit;
			}
		}
	}
}
