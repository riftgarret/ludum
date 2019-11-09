using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace Redninja.Components.Conditions.Expressions
{
	[TestFixture]
	public class ExpressionParserTests : TestBase
	{
		private IExpressionEnv mEnv;		

		[SetUp]
		public void Setup()
		{
			mEnv = Substitute.For<IExpressionEnv>();
		}

		[Test]
		public void ParseNumeric()
		{
			string raw = "32";
			RootExpression subject = new RootExpression(raw);
			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(32));
		}

		[Test]
		public void ParseStat()
		{
			string raw = "self.s_hp";
			RootExpression subject = new RootExpression(raw);

			mEnv.Self.Stats[Stat.HP].Returns(32);

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(32));
		}

		[Test]
		public void ParseLiveStatCurrent()
		{
			string raw = "self.v_hp";
			RootExpression subject = new RootExpression(raw);

			LiveStatContainer container = new LiveStatContainer(100);
			container.Current = 32;

			mEnv.Self.LiveStats[LiveStat.HP].Returns(container);

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(32));
		}

		[Test]
		public void ParseLiveStatPercent()
		{
			string raw = "self.v_hp%";
			RootExpression subject = new RootExpression(raw);
		
			var unit = this.CreateEntity(32, 100);
			mEnv.Self.Returns(unit);

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(32));
		}

		[Test]
		public void ParseEntities_LiveStatCurrent()
		{
			string raw = "bm.unit.v_hp";
			RootExpression subject = new RootExpression(raw);

			var unit1 = this.CreateEntity(32, 100);
			var unit2 = this.CreateEntity(42, 100);

			mEnv.BattleModel.Entities.Returns(new List<IBattleEntity>()
			{
				unit1,
				unit2
			});

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Has.Member(32));
			Assert.That(result, Has.Member(42));
		}

		[Test]
		public void ParseEntities_LiveStatCurrent_AggregateMax()
		{
			string raw = "highest.bm.unit.v_hp";
			RootExpression subject = new RootExpression(raw);

			var unit1 = this.CreateEntity(32, 100);
			var unit2 = this.CreateEntity(42, 100);

			mEnv.BattleModel.Entities.Returns(new List<IBattleEntity>()
			{
				unit1,
				unit2
			});

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(42));
		}

		[Test]
		public void ParseEntities_LiveStatCurrent_AggregateMin()
		{
			string raw = "lowest.bm.unit.v_hp";
			RootExpression subject = new RootExpression(raw);

			var unit1 = this.CreateEntity(32, 100);
			var unit2 = this.CreateEntity(42, 100);

			mEnv.BattleModel.Entities.Returns(new List<IBattleEntity>()
			{
				unit1,
				unit2
			});

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(32));
		}

		[Test]
		public void ParseEntities_LiveStatCurrent_AggregateAvg()
		{
			string raw = "avg.bm.unit.v_hp";
			RootExpression subject = new RootExpression(raw);

			var unit1 = this.CreateEntity(32, 100);
			var unit2 = this.CreateEntity(42, 100);

			mEnv.BattleModel.Entities.Returns(new List<IBattleEntity>()
			{
				unit1,
				unit2
			});

			var result = subject.GetResult(mEnv, mEnv);
			Assert.That(result, Is.EqualTo(37));
		}

	}
}
