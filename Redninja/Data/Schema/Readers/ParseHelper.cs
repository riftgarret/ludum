using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;

namespace Redninja.Data.Schema.Readers
{
	internal static class ParseHelper
	{
		/// <summary>
		/// Parse out pattern so we can put in (0,1),(0,2)... or just 'row'.
		/// </summary>
		/// <param name="patternText"></param>
		/// <returns></returns>
		public static ITargetPattern ParsePattern(string patternText)
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
		public static ActionTime ParseActionTime(List<float> time)
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
		/// <param name="targetCondition"></param>
		/// <returns></returns>
		public static TargetCondition ParseTargetCondition(string targetConditionName)
			=> (TargetCondition)typeof(TargetConditions).GetProperty(targetConditionName).GetValue(null);

		public static OperationProvider ParseOperationProvider(string operationProviderName)
			=> (OperationProvider)typeof(OperationProviders).GetProperty(operationProviderName).GetValue(null);

		/// <summary>
		/// Read this file directly into this json node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static T ReadJson<T>(string filePath)
		{
			string json = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
