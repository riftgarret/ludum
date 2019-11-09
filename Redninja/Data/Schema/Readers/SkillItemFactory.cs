using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Redninja.Components.Combat;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal class SkillItemFactory : IDataItemFactory<ISkill>
	{
		/// <summary>
		/// This enum is used to determine which type of OperationTypes we support and
		/// is used to help deserialize and check.
		/// </summary>
		private enum OperationType
		{
			Damage,
			Debuff
		}


		public ISkill CreateInstance(string dataId, ISchemaStore store)
		{
			CombatSkillSchema item = store.GetSchema<CombatSkillSchema>(dataId);

			CombatSkill skill = new CombatSkill();
			skill.Name = item.Name;			
			skill.Time = ParseHelper.ParseActionTime(item.Time);
			skill.Targets.AddRange(item.TargetingSets.Select(ts => ParseTargetSchema(item, ts, store)));		

			return skill;
		}

		internal SkillTargetingSet ParseTargetSchema(CombatSkillSchema rootSchema, TargetingSetSchema tsSchema, ISchemaStore store)
		{
			SkillTargetingSet targetSet = new SkillTargetingSet(store.SingleInstance<ITargetingRule>(tsSchema.TargetingRuleId));
			foreach (BattleOperationSchema opSchema in tsSchema.Operations)
			{
				targetSet.OpDefinitions.Add(ParseOperation(rootSchema, opSchema));
			}
			return targetSet;
		}


		internal IBattleOperationDefinition ParseOperation(CombatSkillSchema rootSchema, BattleOperationSchema schema)
		{
			if(string.IsNullOrEmpty(schema.OperationType))
				throw new NotSupportedException($"Required missing 'skill.operationType'");
			if (!Enum.TryParse(schema.OperationType, out OperationType opType))
				throw new NotSupportedException($"Invalid 'skill.operationType' detected: {schema.OperationType}");

			switch (opType)
			{
				case OperationType.Damage:
					return ParseDamageParams(rootSchema, schema);
				case OperationType.Debuff:
					return ParseDebuffParams(rootSchema, schema);
			}

			throw new NotImplementedException($"Missing implementation for {schema.OperationType}");
		}

		#region Damage Operation
		private class DamageOpSchema
		{
			public int damage;
			public DamageType damageType;
			public WeaponSlotType slotType;
			public WeaponType weaponType;

		}

		private DamageOperationDefinition ParseDamageParams(CombatSkillSchema rootSchema, BattleOperationSchema schema)
		{
			DamageOpSchema defSchema = schema.Params.ToObject<DamageOpSchema>();
			DamageOperationDefinition def = new DamageOperationDefinition();
			def.SkillDamage = defSchema.damage;
			def.SlotType = defSchema.slotType;
			def.DamageType = defSchema.damageType;
			def.WeaponType = defSchema.weaponType;
			def.ExecutionStart = schema.ExecutionStart;
			def.Stats = ParseHelper.ParseStatsParams(schema.Stats, rootSchema.DefaultStats);
			return def;
		}
		#endregion
		#region Debuff Operation
		private class DebuffOpSchema
		{
			public string buffId;
		}

		private DebuffOperationDefinition ParseDebuffParams(CombatSkillSchema rootSchema, BattleOperationSchema schema)
		{
			DebuffOpSchema defSchema = schema.Params.ToObject<DebuffOpSchema>();
			DebuffOperationDefinition def = new DebuffOperationDefinition();
			def.BuffId = defSchema.buffId;
			def.ExecutionStart = schema.ExecutionStart;
			def.Stats = ParseHelper.ParseStatsParams(schema.Stats, rootSchema.DefaultStats);
			return def;
		}
		#endregion
	}
}
