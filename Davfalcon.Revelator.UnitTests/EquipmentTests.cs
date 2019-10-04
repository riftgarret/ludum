using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;
using Davfalcon.Serialization;
using Davfalcon.Nodes;
using System.Diagnostics;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class EquipmentTests
	{
		private const Attributes STAT = Attributes.STR;
		private const string UNIT_NAME = "UNIT";
		private const string EQUIP_NAME = "EQUIPMENT";

		private IUnit unit;

		[TestInitialize]
		public void TestSetup()
		{
			unit = Unit.Build(b => b
				.SetMainDetails(UNIT_NAME)
				.SetAllBaseStats<Attributes>(10)
				.SetBaseStat(STAT, 15));

			unit.Equipment.AddEquipmentSlot(EquipmentType.Armor);
			unit.Equipment.AddEquipmentSlot(EquipmentType.Accessory);
			unit.Equipment.AddEquipmentSlot(EquipmentType.Accessory);
		}

		private static IEquipment MakeEquip(EquipmentType slot, int add, int mult)
			=> Equipment.Build(slot, b => b
				.SetName(EQUIP_NAME)
				.SetStatAddition(STAT, add)
				.SetStatMultiplier(STAT, mult));

		[TestMethod]
		public void Equip()
		{
			IEquipment equip = MakeEquip(EquipmentType.Armor, 5, 0);

			unit.Equipment.Equip(equip);

			Assert.AreEqual(20, unit.Stats[STAT]);
			Assert.AreEqual(equip, unit.Equipment.GetEquipment(EquipmentType.Armor));
			Assert.AreEqual(UNIT_NAME, unit.Name);
			Assert.AreEqual(EQUIP_NAME, equip.Name);
		}

		[TestMethod]
		public void EquipMultiple()
		{
			IEquipment equip1 = MakeEquip(EquipmentType.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentType.Accessory, 10, 0);
			IEquipment equip3 = MakeEquip(EquipmentType.Accessory, 0, 100);

			unit.Equipment.Equip(equip1);
			unit.Equipment.Equip(equip2, 0);
			unit.Equipment.Equip(equip3, 1);

			Assert.AreEqual(60, unit.Stats[STAT]);
			Assert.AreEqual(equip1, unit.Equipment.GetEquipment(EquipmentType.Armor));
			Assert.AreEqual(equip2, unit.Equipment.GetEquipment(EquipmentType.Accessory, 0));
			Assert.AreEqual(equip3, unit.Equipment.GetEquipment(EquipmentType.Accessory, 1));
		}

		[TestMethod]
		public void GetEquipmentNull()
		{
			Assert.IsNull(unit.Equipment.GetEquipment(EquipmentType.Armor));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Equipped to a mismatched slot.")]
		public void EquipSlotIndexTypeMismatch()
		{
			IEquipment equip = MakeEquip(EquipmentType.Accessory, 10, 0);

			unit.Equipment.EquipSlotIndex(equip, 0);
		}

		[TestMethod]
		public void GrantedBuffs()
		{
			IEquipment equip = Equipment.Build(EquipmentType.Armor, b => b
				.AddBuff(new Buff.Builder().SetName("test1").Build())
				.AddBuff(new Buff.Builder().SetName("test2").Build())
				.AddBuff(new Buff.Builder().SetName("test3").Build()));

			Assert.AreEqual("test1", equip.GrantedBuffs.First().Name);
			Assert.AreEqual("test2", equip.GrantedBuffs.ElementAt(1).Name);
			Assert.AreEqual("test3", equip.GrantedBuffs.ElementAt(2).Name);
		}

		[TestMethod]
		public void GetEquipmentSlots()
		{
			Assert.AreEqual(EquipmentType.Armor, unit.Equipment.EquipmentSlots.First());
			Assert.AreEqual(EquipmentType.Accessory, unit.Equipment.EquipmentSlots.ElementAt(1));
			Assert.AreEqual(EquipmentType.Accessory, unit.Equipment.EquipmentSlots.ElementAt(2));
		}

		[TestMethod]
		public void Serialization()
		{
			IEquipment equip = MakeEquip(EquipmentType.Accessory, 10, 0);

			IEquipment clone = Serializer.DeepClone(equip);
			Assert.AreEqual(equip.Additions[STAT], clone.Additions[STAT]);
		}

		[TestMethod]
		public void WeaponSerialization()
		{
			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.AddDamageTypes(DamageType.Physical, Element.Fire));

			IWeapon clone = Serializer.DeepClone(weapon);

			Assert.AreEqual(weapon.DamageTypes.First(), clone.DamageTypes.First());
		}

		[TestMethod]
		public void EquippedUnitSerialization()
		{
			IEquipment equip1 = MakeEquip(EquipmentType.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentType.Accessory, 10, 0);

			unit.Equipment.Equip(equip1);
			unit.Equipment.Equip(equip2);

			IUnit clone = Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Stats[STAT], clone.Stats[STAT]);

			IEquipment equip3 = MakeEquip(EquipmentType.Accessory, 0, 100);
			clone.Equipment.Equip(equip3, 1);

			Assert.AreEqual(60, clone.Stats[STAT]);
		}
	}
}
