namespace Redninja.Components.Decisions.AI
{

	/// <summary>
	/// Because stats can come from various parts, lets provide a way to get each version.
	/// </summary>
	public interface IStatEvaluator
	{
		int Eval(IBattleEntity entity);
	}

	public class LiveStatEvaluator : IStatEvaluator
	{
		public LiveStat LiveStat { get; }
		public bool IsPercent { get; }

		public LiveStatEvaluator(LiveStat liveStat, bool isPercent)
		{
			LiveStat = liveStat;
			IsPercent = isPercent;
		}

		public int Eval(IBattleEntity entity)
		{
			if (IsPercent)
				return (int)(100 * entity.LiveStats[LiveStat].Percent);
			else
				return entity.LiveStats[LiveStat].Current;
		}

		public override bool Equals(object obj)
		{
			var evaluator = obj as LiveStatEvaluator;
			return evaluator != null &&
				   LiveStat == evaluator.LiveStat &&
				   IsPercent == evaluator.IsPercent;
		}

		public override int GetHashCode()
		{
			var hashCode = 1654115479;
			hashCode = hashCode * -1521134295 + LiveStat.GetHashCode();
			hashCode = hashCode * -1521134295 + IsPercent.GetHashCode();
			return hashCode;
		}
	}

	public class CalculatedStatEvaluator : IStatEvaluator
	{
		public CalculatedStat CalculatedStat { get; }

		public CalculatedStatEvaluator(CalculatedStat calculatedStat)
		{
			CalculatedStat = calculatedStat;
		}

		public int Eval(IBattleEntity entity) => entity.Stats.Calculate(CalculatedStat);

		public override bool Equals(object obj)
		{
			var evaluator = obj as CalculatedStatEvaluator;
			return evaluator != null &&
				   CalculatedStat == evaluator.CalculatedStat;
		}

		public override int GetHashCode()
		{
			return -1097727861 + CalculatedStat.GetHashCode();
		}
	}

	public class StatEvaluator : IStatEvaluator
	{
		public Stat Stat { get; }

		public StatEvaluator(Stat stat)
		{
			Stat = stat;
		}

		public int Eval(IBattleEntity entity) => entity.Stats[Stat];

		public override bool Equals(object obj)
		{
			var evaluator = obj as StatEvaluator;
			return evaluator != null &&
				   Stat == evaluator.Stat;
		}

		public override int GetHashCode()
		{
			return -1100411313 + Stat.GetHashCode();
		}
	}
}
