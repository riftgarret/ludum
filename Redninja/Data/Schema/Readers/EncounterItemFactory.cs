using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Decisions.AI;

namespace Redninja.Data.Schema.Readers
{
	internal class EncounterItemFactory : IDataItemFactory<Encounter>
	{
		public Encounter CreateInstance(string dataId, ISchemaStore store)
		{
			var es = store.GetSchema<EncounterSchema>(dataId);

			Encounter.Builder b = new Encounter.Builder();
			b.SetEnemyGridSize(ParseHelper.ParseCoordinate(es.EnemyGridSize));
			b.SetPlayerGridSize(ParseHelper.ParseCoordinate(es.PlayerGridSize));

			foreach (EncounterEnemy ee in es.Enemies)
			{
				Coordinate position = ParseHelper.ParseCoordinate(ee.Position);
				AIRuleSet behavior = store.SingleInstance<AIRuleSet>(ee.AiBehaviorId);
				IUnit enemy = store.CreateInstance<IUnit>(ee.CharacterId);
				b.AddEnemy(enemy, position, behavior);
			}
			return b.Build();
		}
	}
}
