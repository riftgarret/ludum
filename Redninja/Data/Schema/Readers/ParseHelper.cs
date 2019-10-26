using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Davfalcon.Stats;
using Newtonsoft.Json;
using Redninja.Components.Actions;
using Redninja.Components.Decisions.AI;
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
			if (patternText == null) throw new InvalidOperationException("Cannot parse null pattern");

			switch (patternText.ToLower())
			{
				case "single":
					return TargetPatternFactory.CreatePattern(new Coordinate(0, 0));

			}

			if (patternText.StartsWith("("))
			{
				Match match = Regex.Match(patternText, @"\((?<row>\d+)(\s*),(\s*)(?<col>\d+)\)");
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
			throw new FormatException($"Failed to find a match: {patternText}");			
		}

		public static Coordinate ParseCoordinate(string coordinateText)
		{
			if (coordinateText == null) throw new InvalidOperationException("Coordinate text cannot be null");
			string [] split = coordinateText.Split(',');
			if (split.Length != 2) throw new FormatException($"Coordinate format should be #,# {coordinateText}");

			return new Coordinate(
				int.Parse(split[0].Trim()),
				int.Parse(split[1].Trim()));
		}

		/// <summary>
		/// Convert to ActionTime.
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static ActionTime ParseActionTime(List<float> time)
		{
			if (time == null) throw new InvalidOperationException("Cannot ParseActionTime with null reference");
			if (time.Count() != 3)
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

		public static SkillOperationParameters ParseStatsParams(Dictionary<string, int> original)
		{
			if (original == null) return null;
			SkillOperationParameters paramz = new SkillOperationParameters();
			foreach(var e in original)
			{
				if(!Enum.TryParse<Stat>(e.Key, true, out Stat stat)) throw new FormatException("Invalid Stat found");
				paramz.EditableStats[stat] = e.Value;
				// TODO set combat flags (Projectile, Spell, Healing, Buff)
			}
			return paramz;
		}			

		public static IAITargetCondition ParseAITargetCondition(string conditionParam)
		{
			if (TryParseConditionExpression(conditionParam, out IAITargetCondition cond)) return cond;
			return (IAITargetCondition)typeof(AIConditionFactory).GetProperty(conditionParam).GetValue(null);
		}

		private static bool TryParseConditionExpression(string raw, out IAITargetCondition condition)
		{
			// combat stats condition match
			Match match = Regex.Match(raw, @"(?<stat>[^\s]+)(\s*)(?<op>\>|\<|=|\>=|\<=)(\s*)(?<val>\d+)(?<perc>%?)");
			condition = null;
			if (!match.Success) return false;					
				
			if (!Enum.TryParse<Stat>(match.Groups["stat"].Value, out Stat stats)) return false;
			if (!TryParseOperatorType(match.Groups["op"].Value, out AIValueConditionOperator op)) return false;
			if (!int.TryParse(match.Groups["val"].Value, out int value)) return false;
			AIConditionType condType = match.Groups["perc"].Value.Equals("%") ? 
				AIConditionType.CombatStatPercent : AIConditionType.CombatStatCurrent;

			condition = AIConditionFactory.CreateCombatStatCondition(value, stats, op, condType);
			return true;
		}

		private static bool TryParseOperatorType(string raw, out AIValueConditionOperator op)
		{
			switch(raw)
			{
				case ">":
					op = AIValueConditionOperator.GT;
					return true;
				case ">=":
					op = AIValueConditionOperator.GTE;
					return true;
				case "<":
					op = AIValueConditionOperator.LT;
					return true;
				case "<=":
					op = AIValueConditionOperator.LTE;
					return true;
				case "=":
					op = AIValueConditionOperator.EQ;
					return true;
				default:
					op = AIValueConditionOperator.EQ;
					return false;
			}
		}

		public static IAITargetPriority ParseAITargetPriority(string priorityFactoryProperty)
			=> (IAITargetPriority)typeof(AITargetPriorityFactory).GetProperty(priorityFactoryProperty).GetValue(null);

		
		public static T CreateInstance<T>(string theNamespace, string theClass) where T : class
		{
			Assembly assem = typeof(ParseHelper).Assembly;
			Type t = assem.GetType(theNamespace + "." + theClass);
			return (T) Activator.CreateInstance(t);
		}

		public static void ApplyProperties(object instance, Dictionary<string, object> properties, bool parseEnums = false)
		{
			var modProps = ConvertEnums(properties);
			modProps?.ForEach(e =>
			{
				Type type = instance.GetType();
				PropertyInfo prop = type.GetProperty(e.Key);
				prop.SetValue(instance, e.Value, null);
			});
		}		

		internal static Dictionary<string, object> ConvertEnums(Dictionary<string, object> dict)
		{				
			List<Type> detectedEnums = new List<Type>()
			{
				typeof(Stat)
			};

			return dict?.ToDictionary(e => e.Key, e =>
			{
				if (e.Value is string)
				{
					string word = (string)e.Value;
					Type type = detectedEnums.Find(x => word.StartsWith(x.Name + "."));
					if (type != null)
					{
						return Enum.Parse(type, word.Substring(type.Name.Length + 1));
					}
				}
				return e.Value;
			});			
		}

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
