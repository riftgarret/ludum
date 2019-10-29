using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Redninja.Data.Schema;
using Redninja.Data.Schema.Readers;

namespace Redninja.Data
{
	internal class DataManager : IDataManager, ISchemaStore
	{		
		private Dictionary<Tuple<Type, string>, object> cachedStaticMap = new Dictionary<Tuple<Type, string>, object>();
		private Dictionary<Tuple<Type, string>, IDataSource> schemaMap = new Dictionary<Tuple<Type, string>, IDataSource>();
		private Dictionary<Type, object> factoryMap = new Dictionary<Type, object>();

		private IDataItemFactory<TYPE> GetFactory<TYPE>() where TYPE : class 
			=> (IDataItemFactory<TYPE>) factoryMap.GetOrThrow(typeof(TYPE), "factoryMap");

		public TYPE CreateInstance<TYPE>(string dataId) where TYPE : class
			=> GetFactory<TYPE>().CreateInstance(dataId, this);

		

		public TYPE SingleInstance<TYPE>(string dataId) where TYPE : class
		{
			var key = Tuple.Create(typeof(TYPE), dataId);
			
			if (!cachedStaticMap.TryGetValue(key, out object cached))
			{
				cached = cachedStaticMap[key] = CreateInstance<TYPE>(dataId);
			}
			return (TYPE) cached;
		}			

		public SCHEMA_TYPE GetSchema<SCHEMA_TYPE>(string dataId) where SCHEMA_TYPE : IDataSource 
			=> (SCHEMA_TYPE)schemaMap.GetOrThrow(Tuple.Create(typeof(SCHEMA_TYPE), dataId), "schemaMap");

		public DataManager()
		{
			AddDataItemFactory(new AIBehaviorItemFactory());
			AddDataItemFactory(new AIRuleItemFactory());
			AddDataItemFactory(new BuffItemFactory());
			AddDataItemFactory(new CharacterItemFactory());						
			AddDataItemFactory(new EncounterItemFactory());
			AddDataItemFactory(new SkillItemFactory());
			AddDataItemFactory(new TargetingRuleItemFactory());
		}

		public void AddDataItemFactory<TYPE>(IDataItemFactory<TYPE> factory) where TYPE : class
			=> factoryMap[typeof(TYPE)] = factory;

		public void LoadJson(string json)
		{
			List<RootSchema> schemas = JsonConvert.DeserializeObject<List<RootSchema>>(json, new RootSchemaConverter());
			schemas.ForEach(schema => schema.Data.ForEach(item => schemaMap[Tuple.Create(schema.DataType, item.DataId)] = item));
		}
		//public void LoadJson(string configPath) => new JsonSchemaLoader(configPath).Read(this);
		//public void Load(IDataLoader reader) => reader.Load(this);
	}
}
