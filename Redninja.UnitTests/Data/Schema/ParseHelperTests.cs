using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Operations;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.UnitTests
{
	[TestFixture]
	public class ParseHelperTests
	{
		[Test]
		public void GetTargetCondition()
		{
			TargetCondition none = ParseHelper.ParseTargetCondition("None");
			Assert.AreEqual(TargetConditions.None, none);
			Assert.IsTrue(none(null, null));
		}

		[Test]
		public void GetOperationProvider()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			ITargetResolver mTarget = Substitute.For<ITargetResolver>();
			ISkill mSkill = Substitute.For<ISkill>();

			OperationProvider damage = ParseHelper.ParseOperationProvider("Damage");
			Assert.AreEqual(OperationProviders.Damage, damage);
			Assert.IsInstanceOf<DamageOperation>(damage(mEntity, mTarget, mSkill));
		}
	}
}
