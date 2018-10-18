using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;

namespace Redninja.Data.Schema.Readers
{
	internal static class CharacterReader
	{		
		public static void ReadRoot(List<CharacterSchema> characters, IEditableDataManager manager)
		{
			foreach(CharacterSchema cs in characters)
			{
				IUnit unit = Unit.Build(b =>
				{
					b.SetMainDetails(cs.Name, cs.Class, cs.Level);

					foreach (var item in cs.Stats)
					{
						b.SetBaseStat(item.Key, item.Value);
					}

					return b;
				});

				manager.NPCUnits[cs.DataId] = unit;
			}
		}
	}
}
