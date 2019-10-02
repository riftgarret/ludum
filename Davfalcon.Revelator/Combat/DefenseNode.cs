using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	internal class DefenseNode : NodeEnumerableBase, IDefenseNode
	{
		private readonly IList<INode> nodes = new List<INode>(2);

		public override string Name => Defender.Name;

		public IUnit Defender { get; }
		public IDamageNode IncomingDamage { get; }
		public INode Base => IncomingDamage;
		public INode Addend { get; }
		public INode Multiplier { get; }

		public DefenseNode(IUnit defender, IDamageNode incomingDamage, ICombatResolver resolver)
		{
			ICombatOperations operations = resolver?.Operations ?? throw new ArgumentNullException();

			Defender = defender ?? throw new ArgumentNullException();
			IncomingDamage = incomingDamage ?? throw new ArgumentNullException();

			IEnumerable<Enum> defensiveStats = resolver.GetDamageDefendingStats(IncomingDamage.Source);

			if (defensiveStats.Count() > 1)
			{
				List<INode> nodes = new List<INode>();
				foreach (Enum stat in defensiveStats)
				{
					nodes.Add(Defender.StatsDetails.GetStatNode(stat));
				}
				Multiplier = new AggregatorNode("Defense", nodes, operations);
			}
			else if (defensiveStats.Count() == 1)
			{
				Multiplier = Defender.StatsDetails.GetStatNode(defensiveStats.First());
			}

			nodes.Add(Base);
			if (Multiplier != null) nodes.Add(Multiplier);

			Value = operations.ScaleInverse(Base.Value, Multiplier?.Value ?? operations.AggregateSeed);
		}

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();

		protected override string GetTypeName() => "Defense";
	}
}
