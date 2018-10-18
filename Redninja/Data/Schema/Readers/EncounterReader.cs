using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;

namespace Redninja.Data.Schema.Readers
{
	internal static class EncounterReader
	{
		public static void ReadRoot(List<EncounterSchema> encounters, IEditableDataManager manager)
		{
			foreach(EncounterSchema es in encounters)
			{
				Encounter.Builder b = new Encounter.Builder();
				b.SetEnemyGridSize(ParseHelper.ParseCoordinate(es.EnemyGridSize));
				b.SetPlayerGridSize(ParseHelper.ParseCoordinate(es.PlayerGridSize));

				foreach(EncounterEnemy ee in es.Enemies)
				{
					Coordinate position = ParseHelper.ParseCoordinate(ee.Position);
					AIRuleSet behavior = manager.AIBehavior[ee.AiBehaviorId];
					IUnit enemy = manager.NPCUnits[ee.CharacterId];
					b.AddEnemy(enemy, position, behavior);
				}

				manager.Encounters[es.DataId] = b.Build();
			}
		}
	}
}
