﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Davfalcon;
using Davfalcon.Stats;
using Newtonsoft.Json;
using Redninja.Components.Actions;
using Redninja.Components.Conditions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;

namespace Redninja.Data.Schema.Readers
{
	internal static class ParseHelper
	{
		private static readonly List<Type> _KNOWN_ENUM_TYPES = new List<Type>()
			{
				typeof(Stat),
				typeof(LiveStat),
				typeof(CalculatedStat),
				typeof(DamageType),
				typeof(WeaponSlotType),
				typeof(WeaponType)
			};

		private static ConditionParser _CONDITION_PARSER_INSTANCE = new ConditionParser();

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
		/// Create a new Condition from raw input. See Condition workflow on wiki.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static ICondition ParseCondition(string input)
		{
			if (_CONDITION_PARSER_INSTANCE.TryParseCondition(input, out ICondition condition)) return condition;
			throw new InvalidOperationException($"Unable to parse condition: {input}");
		}

		/// <summary>
		/// Convert enum to instance.
		/// </summary>
		/// <param name="targetCondition"></param>
		/// <returns></returns>
		public static TargetCondition ParseTargetCondition(string targetConditionName)
			=> (TargetCondition)typeof(TargetConditions).GetProperty(targetConditionName).GetValue(null);

		public static IStats ParseStatsParams(Dictionary<string, int> operationStats, Dictionary<string, int> defaultStats)
		{
			Dictionary<string, int> merged = new Dictionary<string, int>();
			defaultStats.ForEach(e => merged[e.Key] = e.Value);
			operationStats.ForEach(e => merged[e.Key] = e.Value);
			return ParseStatsParams(merged);
		}

		public static IStats ParseStatsParams(Dictionary<string, int> original)
		{
			if (original == null) return EmptyStats.INSTANCE;
			StatsMap stats = new StatsMap();
			//SkillOperationParameters paramz = new SkillOperationParameters();
			foreach(var e in original)
			{
				if(!Enum.TryParse(e.Key, true, out Stat stat)) throw new FormatException($"Invalid Stat found {e.Key}");
				stats[stat] = e.Value;
				// TODO set combat flags (Projectile, Spell, Healing, Buff)
			}
			return stats;
		}			

		public static IAITargetPriority ParseAITargetPriority(string priorityFactoryProperty)
			=> (IAITargetPriority)typeof(AITargetPriorityFactory).GetProperty(priorityFactoryProperty).GetValue(null);

		
		public static T CreateInstance<T>(string theNamespace, string theClass) where T : class
		{
			Assembly assem = typeof(ParseHelper).Assembly;
			Type t = assem.GetType(theNamespace + "." + theClass);
			return (T) Activator.CreateInstance(t);
		}

		public static void ApplyProperties(object instance, Dictionary<string, object> properties)
		{
			var modProps = ConvertEnums(properties);
			modProps?.ForEach(e =>
			{
				Type type = instance.GetType();
				PropertyInfo prop = type.GetProperty(e.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
				try
				{
					prop.SetValue(instance, e.Value, null);
				}
				catch(Exception ex)
				{
					throw new SystemException($"Failed to Apply property: {e.Key} : {e.Value}", ex);
				}
			});
		}				

		internal static bool TryParseStatRaw(string raw, out Enum val)
		{
			try
			{
				val = ParseStatRaw(raw);
				return true;
			}
			catch (InvalidOperationException e)
			{
				val = default;
				return false;
			}
		}

		internal static IStatEvaluator CreateStatEval(Enum someStat, bool isPercent)
		{			
			if (someStat is Stat)
				return new StatEvaluator((Stat)someStat);
			else if (someStat is CalculatedStat)
				return new CalculatedStatEvaluator((CalculatedStat)someStat);
			else if (someStat is LiveStat)
				return new LiveStatEvaluator((LiveStat)someStat, isPercent);
			else
				throw new InvalidOperationException("Unable to pick stat expression for " + someStat);
		}

		internal static Enum ParseStatRaw(string raw)
		{
			Type type = _KNOWN_ENUM_TYPES.Find(x => raw.StartsWith(x.Name + "."));
			if (type != null)
			{
				return (Enum) Enum.Parse(type, raw.Substring(type.Name.Length + 1));
			}
			throw new InvalidOperationException("Unable to parse: " + raw);
		}

		internal static Dictionary<string, object> ConvertEnums(Dictionary<string, object> dict)
		{
			List<Type> detectedEnums = _KNOWN_ENUM_TYPES;
			
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
