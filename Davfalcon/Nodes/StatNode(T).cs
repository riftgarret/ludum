using System;

namespace Davfalcon.Nodes
{
	public class StatNode<T> : NodeEnumerableBase, IStatNode<T> where T : INameable
	{
		private readonly string name;
		private readonly IStats stats;

		public Enum Stat { get; }
		public T Source { get; }

		public StatNode(string name, IStats stats, Enum stat)
			: this(default(T), stats, stat)
		{
			this.name = name;
		}

		public StatNode(T source, IStats stats, Enum stat)
		{
			name = null;
			this.stats = stats;

			Stat = stat ?? throw new ArgumentNullException();
			Source = source;

			Name = $"{Source?.Name ?? name} {Stat}";
			Value = stats?[Stat] ?? 0;
		}
		
		protected override string GetTypeName() => "Stat";

		public static StatNode<TSource> From<TSource>(TSource source, IStats stats, Enum stat) where TSource : INameable
			=> new StatNode<TSource>(source, stats, stat);

		public static StatNode<TSource> From<TSource>(TSource source, Enum stat) where TSource : IStatsHolder, INameable
			=> new StatNode<TSource>(source, source.Stats, stat);
	}
}
