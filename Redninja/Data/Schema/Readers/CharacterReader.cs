using System.Collections.Generic;

namespace Redninja.Data.Schema.Readers
{
	internal static class CharacterReader
	{
		public static void ReadCharacters(List<CharacterSchema> characters, IEditableDataManager manager)
		{
			foreach(CharacterSchema ch in characters)
			{
				//Unit.Builder builder = new Unit.Builder(); // nope doesnt work..
				// TODO need someway to create characters..
			}
		}
	}
}
