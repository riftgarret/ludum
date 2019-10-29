using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Redninja.Data.Schema
{
	internal class RootSchemaConverter : JsonConverter<RootSchema>
	{
		public override RootSchema ReadJson(JsonReader reader, Type objectType, RootSchema existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject item = JObject.Load(reader);

			string typeName = item["type"].Value<string>();

			Type rootType = typeof(RootSchema);
			Type dataType = rootType.Assembly.GetType(rootType.Namespace + "." + typeName);

			var jsonData = item["data"];
			var parsedData = jsonData.Select(x => {				
				try
				{
					return (IDataSource)x.ToObject(dataType, serializer);
				} catch(InvalidCastException ie)
				{
					throw new InvalidCastException($"Invalid cast from type: {typeName} To IDataSource", ie);
				}
				}).ToList();

			return new RootSchema()
			{
				DataType = dataType,
				Data = parsedData
			};
		}

		public override void WriteJson(JsonWriter writer, RootSchema value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
