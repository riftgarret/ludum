using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Combat;
using Davfalcon.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class CombatTests
	{
		private static IUnit MakeUnit()
			=> Unit.Build(b => b
				.SetBaseStat(Attributes.STR, 15)
				.SetBaseStat(Attributes.VIT, 15)
				.SetBaseStat(CombatStats.ATK, 20)
				.SetBaseStat(CombatStats.DEF, 20)
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.MP, 100));

		[TestMethod]
		public void InitializeVolatileStats()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.Build();
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.VolatileStats[CombatStats.HP]);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.VolatileStats[CombatStats.MP]);
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5));

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(5, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithBonus()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5)
				.SetBonusDamageStat(Attributes.STR));

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(20, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithScaling()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.Build();
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5)
				.SetBonusDamageStat(Attributes.STR)
				.AddDamageType(DamageType.Physical));

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(24, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateReceivedDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			Damage d = new Damage(10, "", DamageType.Physical);

			Assert.AreEqual(10, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedDamage_WithResist()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.Build();
			IUnit unit = MakeUnit();

			Damage d = new Damage(10, "", DamageType.Physical);

			Assert.AreEqual(8, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void ReceiveDamage()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageResourceRule(DamageType.Physical, CombatStats.HP)
				.AddVolatileStat(CombatStats.HP)
				.Build();
			IUnit unit = MakeUnit();
			combat.Initialize(unit);

			Damage d = new Damage(10, "", DamageType.Physical);
			IEnumerable<StatChange> h = combat.ReceiveDamage(unit, d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.VolatileStats[CombatStats.HP], -h.First().Value);
			Assert.AreEqual(unit.Name, h.First().Unit);
		}

		[TestMethod]
		public void ApplyBuff()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.Build();

			IUnit unit = MakeUnit();
			IBuff b = new Buff.Builder()
				.SetName("Test buff")
				.SetStatAddition(CombatStats.ATK, 10)
				.Build();

			combat.ApplyBuff(unit, b);

			Assert.AreEqual(30, unit.Stats[CombatStats.ATK]);
		}

		[TestMethod]
		public void SerializeVolatileStats()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.Build();
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			IUnit clone = unit.DeepClone();

			Assert.AreEqual(unit.VolatileStats[CombatStats.HP], clone.VolatileStats[CombatStats.HP]);
			Assert.AreEqual(unit.VolatileStats[CombatStats.MP], clone.VolatileStats[CombatStats.MP]);
		}
	}
}
