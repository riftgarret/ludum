using Redninja.Components.Buffs;

namespace Redninja.Data.Schema.Readers
{
	// TODO refactor for active buffs
	internal class BuffItemFactory : IDataItemFactory<IBuff>
	{
		public IBuff CreateInstance(string dataId, ISchemaStore store)
		{
			BuffSchema buffSchema = store.GetSchema<BuffSchema>(dataId);

			ActiveBuff buff = new ActiveBuff()
			{
				Properties = buffSchema.Properties,
				Name = buffSchema.Name
			};

			if (!string.IsNullOrEmpty(buffSchema.Executor))
			{
				buff.Behavior = ParseHelper.CreateInstance<IBuffExecutionBehavior>("Redninja.Components.Buffs.Behaviors", buffSchema.Executor);
				ParseHelper.ApplyProperties(buff.Behavior, buffSchema.ExectorProps);
			}

			return buff;
		}
	}
}
