using System.Collections.Generic;
using NUnit.Framework;

namespace Redninja.Components.Utils.UnitTests
{
	[TestFixture]
	public class TextUtilsTests
	{
		[TestCase("apple,apple,orange,apple", "apple a,apple b,orange,apple c")]
		[TestCase("goblin,goblin", "goblin a,goblin b")]
		[TestCase("doyo,rice,rift", "doyo,rice,rift")]
		public void CreateUniqueNames(string inputRaw, string expectedRaw)
		{
			var input = new List<string>(inputRaw.Split(','));
			var expected = new List<string>(expectedRaw.Split(','));

			var result = TextUtils.CreateUniqueNames(input);
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
