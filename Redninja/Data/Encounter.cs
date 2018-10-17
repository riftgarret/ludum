using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Utils;

namespace Redninja.Data
{
	public class Encounter
	{
		public Coordinate PlayerGridSize { get; private set; }
		public Coordinate EnemyGridSize { get; private set; }
		public List<EnemyMeta> EnemyMetas { get; private set; }

		public class EnemyMeta
		{
			public string DisplayName { get; internal set; }
			public IUnit Character { get; internal set; }// or IUnitModel?
			public Coordinate InitialPosition { get; internal set; }
			public AIRuleSet AiBehavior { get; internal set; }
		}

		public class Builder : IBuilder<Encounter>
		{
			private Encounter encounter;

			public Builder() => Reset();

			public Builder Reset()
			{
				encounter = new Encounter();
				encounter.EnemyMetas = new List<EnemyMeta>();
				return this;
			}

			private Builder Apply(Action<Encounter> func)
			{
				func(encounter);
				return this;
			}

			public Builder SetEnemyGridSize(Coordinate coordinate)
				=> Apply(e => e.EnemyGridSize = coordinate);

			public Builder SetPlayerGridSize(Coordinate coordinate)
				=> Apply(e => e.PlayerGridSize = coordinate);

			public Builder AddEnemy(IUnit enemy, Coordinate initPosition, AIRuleSet behavior)
				=> Apply(e => e.EnemyMetas.Add(new EnemyMeta()
				{
					Character = enemy,
					AiBehavior = behavior,
					InitialPosition = initPosition
				}));

			private void BindUniqueNames()
			{
				List<string> uniqueNames = TextUtils.CreateUniqueNames(encounter.EnemyMetas.Select(x => x.Character.Name).ToList());
				for (int i = 0; i < uniqueNames.Count(); i++)
				{
					encounter.EnemyMetas[i].DisplayName = uniqueNames[i];
				}
			}

			public Encounter Build()
			{
				BindUniqueNames();
				Encounter retEncounter = encounter;
				Reset();
				return retEncounter;
			}
		}
	}
}
