using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Redninja.UnitTests.Data.Schema
{
	[TestFixture]
	public class RootSchemaTest
	{		

		[Test]
		public void testDeserialize()
		{
			string data = @"{
  'Email': 'james@example.com',
  'roles': [
    {'alpha': 'User'},
    {'alpha': 'beta'}
  ]
}";
			SomeClass item = JsonConvert.DeserializeObject<SomeClass>(data, new SchemaConverter());
		}

		class SomeClass
		{
			public List<object> data { get; set; }
		}

		class InnerClass
		{
			public string alpha;
		}

		class SchemaConverter : JsonConverter<SomeClass>
		{
			public override SomeClass ReadJson(JsonReader reader, Type objectType, SomeClass existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				SomeClass c = new SomeClass();

				JObject item = JObject.Load(reader);
				var theData = item["roles"];

				Type type = typeof(InnerClass);

				c.data = theData.Select(x => x.ToObject(type)).ToList();

				return c;
			}

			public override void WriteJson(JsonWriter writer, SomeClass value, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}
		}
	}
}
