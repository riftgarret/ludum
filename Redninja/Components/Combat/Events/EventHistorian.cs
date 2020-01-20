using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Davfalcon.Stats;

namespace Redninja.Components.Combat.Events
{
	public class EventHistorian
	{
		private StatsMap stats = new StatsMap();
		private IDictionary<Enum, Func<IStats, int>> evaluators = new Dictionary<Enum, Func<IStats, int>>();

		public IDictionary<Enum, IDictionary<string, int>> PropertyDictionary { get; private set; } = new Dictionary<Enum, IDictionary<string, int>>();
		public IStats Stats => stats;		

		public void AddPropery(Enum e, string name, int value)
		{			
			// historical maps name to value
			var dict = PropertyDictionary.GetOrNew(e, () => new Dictionary<string, int>());			
			dict[name] = value;
		}

		public void CalculateEvalators()
		{
			// first extract all values from properties
			PropertyDictionary.ForEach((kvp) => stats[kvp.Key] = (int) kvp.Value.Values.Sum());

			// run evaluators and save those values
			evaluators.ForEach((kvp) => stats[kvp.Key] = kvp.Value.Invoke(stats));
		}

		public void RegisterEvaluator(Enum e, Func<IStats, int> evaluator) => evaluators[e] = evaluator;				
	}
}
