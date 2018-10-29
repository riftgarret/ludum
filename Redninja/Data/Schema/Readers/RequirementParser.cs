using System;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Operators;
using Redninja.Logging;

namespace Redninja.Data.Schema.Readers
{
	internal class RequirementParser
	{
		internal bool TryParseRequirement(string raw, out IOperatorCountRequirement requirement)
		{

			string rawLower = raw.ToLower();

			if(rawLower.Equals("any"))
			{
				requirement = AnyOpRequirement.INSTANCE;
				return true;
			}

			if(rawLower.Equals("all"))
			{
				requirement = AllOpRequirement.INSTANCE;
				return true;
			}

			string[] opCondSplit = rawLower.Split(' ');

			if (!ConditionOpParser.TryParseOperatorType(opCondSplit[0], out ConditionOperatorType opType))
				return FailWithLog($"Unable to parse operator: {raw}", out requirement);

			if(!int.TryParse(opCondSplit[1], out int value))
				return FailWithLog($"Unable to parse int: {raw}", out requirement);

			requirement = new OpCountRequirement(opType, value);
			return true;
		}

		private bool FailWithLog(string log, out IOperatorCountRequirement requirement)
		{
			RLog.E(this, log);
			requirement = null;
			return false;
		}
	}
}
