using System;
using Davfalcon;
using Davfalcon.Stats;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{	
	/// <summary>
	/// This class manages snapshotting stats of when a operation is executing. Allowing
	/// classes responsible for determining how damage properties should be calculated to
	/// populate via CaptureSourceStat.
	/// 
	/// We expose the unit itself to allow conditional properties to be checked.
	/// 
	/// The result of this operation should be creating a OperationResult.
	/// </summary>
	public class OperationContext
	{			
		public readonly IBattleEntity SourceUnit;
		public readonly IBattleEntity TargetUnit;
		public readonly IStatsProvider SourceStats;
		public readonly IStatsProvider TargetStats;
		private readonly EventHistorian historian;

		public OperationContext(IBattleEntity source, IBattleEntity target, IStatSource skillSource)
		{
			SourceUnit = source;
			TargetUnit = target;
			historian = new EventHistorian();
			SourceStats = SnapshotSources(source, skillSource);
			TargetStats = SnapshotSources(target);
		}

		public IStatsProvider SnapshotSources(IStatsProvider provider, IStatSource skillSource = null)
		{
			StatsProvider snapshot = new StatsProvider();
			provider.AllSources().ForEach(x => snapshot.AddSource(new SourceSnapshot(x)));
			if (skillSource != null) snapshot.AddSource(skillSource);
			return snapshot;
		}

		public void CaptureSourceStat(Enum opKey, Enum stat)
		{
			SourceStats.GetSources(stat).ForEach(x => historian.AddPropery(opKey, x.Name, x.Stats[stat]));
		}

		public void CaptureTargetStat(Enum opKey, Enum stat)
		{
			TargetStats.GetSources(stat).ForEach(x => historian.AddPropery(opKey, x.Name, x.Stats[stat]));
		}

		public SkillOperationResult BuildSkillResult(DamageType damageType)
		{
			return new SkillOperationResult(damageType, historian);
		}

		public TickOperationResult BuildTickResult(DamageType damageType)
		{
			return new TickOperationResult(damageType, historian);
		}

		private class SourceSnapshot : IStatSource
		{
			public string Name { get; }

			public IStats Stats { get; }

			public SourceSnapshot(IStatSource source)
			{
				Name = source.Name;
				Stats = new StatsMap();

				source.Stats.StatKeys.ForEach(x => ((StatsMap)Stats)[x] = source.Stats[x]);
			}
		}
	}
	
}
