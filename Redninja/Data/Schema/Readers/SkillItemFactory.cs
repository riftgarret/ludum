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
		public ISkill CreateInstance(string dataId, ISchemaStore store)
		{
			CombatSkillSchema item = store.GetSchema<CombatSkillSchema>(dataId);

			CombatSkill skill = new CombatSkill();
			skill.Time = ParseHelper.ParseActionTime(item.Time);
			skill.Targets.AddRange(item.TargetingSets.Select(ts => ParseTargetSchema(ts, store)));

			return skill;
		}		

		internal SkillTargetingSet ParseTargetSchema(TargetingSetSchema tsSchema, ISchemaStore store)
		{
			SkillTargetingSet targetSet = new SkillTargetingSet(store.SingleInstance<ITargetingRule>(tsSchema.TargetingRuleId));
			foreach (BattleOperationSchema opSchema in tsSchema.Ops)
			{
				targetSet.OpDefinitions.Add(ParseOperation(opSchema));
			}
			return targetSet;
		}

		internal IBattleOperationDefinition ParseOperation(BattleOperationSchema schema)
		{
			if (schema.OpType.Equals("Damage", StringComparison.CurrentCultureIgnoreCase))
				return ParseDamageParams(schema);
			else
				throw new InvalidOperationException($"Unknown OpType for skill: {schema.OpType}");
		}

		private class DamageOpSchema
		{
			public int damage;
			public DamageType damageType;
			public WeaponSlotType slotType;
			public WeaponType weaponType;
		}

		private DamageOperationDefinition ParseDamageParams(BattleOperationSchema schema)
		{
			DamageOpSchema defSchema = schema.Params.ToObject<DamageOpSchema>();
			DamageOperationDefinition def = new DamageOperationDefinition();
			def.SkillDamage = defSchema.damage;
			def.SlotType = defSchema.slotType;
			def.DamageType = defSchema.damageType;
			def.WeaponType = defSchema.weaponType;
			def.ExecutionStart = schema.ExecutionStart;
			return def;
		}


	}
}
