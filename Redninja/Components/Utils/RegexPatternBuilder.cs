using System;
using System.Text;
namespace Redninja.Components.Utils
{
	public class RegexPatternBuilder
	{
		private StringBuilder sb = new StringBuilder();

		private RegexPatternBuilder()
		{
		}

		private RegexPatternBuilder Self(Action<StringBuilder> action)
		{
			action(sb);
			return this;
		}

		public RegexPatternBuilder AddWhiteSpaceOptional() => Self(sb => sb.Append(@"(\s*)"));

		public RegexPatternBuilder AddNonCapture(string regex) => Self(sb => sb.Append(regex));

		public OptionSetBuilder StartOptionSet()
			=> new OptionSetBuilder(this);

		public RegexPatternBuilder AddCapture(string key, string regex)
			=> Self(sb => sb.Append($@"(<{key}>{regex})"));

		public string Build()
		{
			sb.Append("$"); // end line
			return sb.ToString();
		}

		public static RegexPatternBuilder Begin() {
			RegexPatternBuilder self = new RegexPatternBuilder();
			self.sb.Append("^");
			return self;
		}

		public class OptionSetBuilder
		{
			private RegexPatternBuilder builder;
			internal OptionSetBuilder(RegexPatternBuilder parent)
			{
				builder = parent;
				builder.sb.Append("(");
			}

			private OptionSetBuilder Self(Action<RegexPatternBuilder> action) {
				action(builder);
				return this;
			}

			public OptionSetBuilder AddWhiteSpaceOptional() => Self(b =>b.AddWhiteSpaceOptional());

			public OptionSetBuilder AddNonCapture(string regex) => Self(b => b.AddNonCapture(regex));

			public OptionSetBuilder AddCapture(string key, string regex) => Self(b => b.AddCapture(key, regex));

			public OptionSetBuilder NextOption() => Self(b => AddNonCapture("|"));

			public RegexPatternBuilder EndOptions() {
				builder.AddNonCapture(")");
				return builder;
			}
		}
	}
}
