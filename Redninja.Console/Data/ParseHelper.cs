﻿using Redninja.Actions;
using Redninja.Logging;
using Redninja.Operations;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Redninja.Skills.SkillOperationDefinition;

namespace Redninja.ConsoleDriver.Data
{
	internal static class ParseHelper
	{
		/// <summary>
		/// Parse out pattern so we can put in (0,1),(0,2)... or just 'row'.
		/// </summary>
		/// <param name="patternText"></param>
		/// <returns></returns>
		internal static ITargetPattern ParsePattern(string patternText)
		{
			switch (patternText.ToLower())
			{
				case "single":
					return TargetPatternFactory.CreatePattern(new Coordinate(0, 0));
					// etc
			}

			if (patternText.StartsWith("("))
			{
				Match match = Regex.Match(patternText, @"\((?<row>\d),(\s*)(?<col>\d)\)");
				List<Coordinate> patternCoordinates = new List<Coordinate>();
				while (match.Success)
				{
					var coordinate = new Coordinate(
						int.Parse(match.Groups["row"].Value),
						int.Parse(match.Groups["col"].Value));
					patternCoordinates.Add(coordinate);
					match = match.NextMatch();
				}

				if (patternCoordinates.Count() > 0)
				{
					return TargetPatternFactory.CreatePattern(patternCoordinates.ToArray());
				}
			}

			RLog.D("ParsePattern", $"Failed to find a match: {patternText}");

			return TargetPatternFactory.CreatePattern(new Coordinate(0, 0));
		}

		/// <summary>
		/// Convert to ActionTime.
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		internal static ActionTime ParseActionTime(List<float> time)
		{
			if(time.Count() != 3)
			{
				throw new FormatException("Invalid action time, needs to be exactly 3 floats");
			}

			return new ActionTime(time[0], time[1], time[2]);
		}

		/// <summary>
		/// Convert enum to instance.
		/// </summary>
		/// <param name="tce"></param>
		/// <returns></returns>
		internal static TargetCondition ParseTargetCondition(TargetConditionEnum tce)
		{
			switch(tce)
			{
				case TargetConditionEnum.MustBeAlive:
					return TargetConditions.MustBeAlive;

				case TargetConditionEnum.None:
				default:
					return TargetConditions.None;
			}
		}

		internal static OperationProvider ParseOperation(OperationEnum op)
		{
			switch(op)
			{
				default:
				case OperationEnum.Damage:
					return (e,t,s) => new DamageOperation(e,t,s);
			}
		}
	}
}
