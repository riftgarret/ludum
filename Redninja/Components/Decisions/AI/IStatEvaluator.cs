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
	}

	public class CalculatedStatEvaluator : IStatEvaluator
	{
		public CalculatedStat CalculatedStat { get; }

		public CalculatedStatEvaluator(CalculatedStat calculatedStat)
		{
			CalculatedStat = calculatedStat;
		}

		public int Eval(IBattleEntity entity) => entity.Stats.Calculate(CalculatedStat);
	}

	public class StatEvaluator : IStatEvaluator
	{
		public Stat Stat { get; }

		public StatEvaluator(Stat stat)
		{
			Stat = stat;
		}

		public int Eval(IBattleEntity entity) => entity.Stats[Stat];
	}
}
