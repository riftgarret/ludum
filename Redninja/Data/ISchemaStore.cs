namespace Redninja.Data
{
	public interface ISchemaStore : IDataFactory
	{
		SCHEMA_TYPE GetSchema<SCHEMA_TYPE>(string dataId) where SCHEMA_TYPE : IDataSource;	
	}
}
