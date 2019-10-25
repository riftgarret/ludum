using Redninja.Components.Buffs;
using Redninja.Data.Schema;

namespace Redninja.Data.Factories
{
	// can probably be static
	internal class BuffFactory
	{
		public IBuff CreateActiveBuff(BuffSchema schema)
		{
			ActiveBuff buff = new ActiveBuff()
			{
				Properties = schema.Properties
			};

			// set other things like stats/execution, eg:
			// buff.StatModifications[StatModType.Additive][Stat.ATK] =

			return buff;
		}
	}
}
