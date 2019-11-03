using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema.Readers
{
	internal class CharacterItemFactory : IDataItemFactory<IUnit>
	{
		public IUnit CreateInstance(string dataId, ISchemaStore store)
		{
			var cs = store.GetSchema<CharacterSchema>(dataId);
			var unit = new Unit();
			foreach (var item in cs.Stats)
			{
				unit.BaseStats[item.Key] = item.Value;
			}
			unit.Name = cs.Name;			
			//IUnit unit = Unit.Build(b =>
			//{
			//	b.SetMainDetails(cs.Name, cs.Class, cs.Level);

			//	foreach (var item in cs.Stats)
			//	{
			//		b.SetBaseStat(item.Key, item.Value);
			//	}

			//	return b;
			//});

			return unit;
		}
	}
}
