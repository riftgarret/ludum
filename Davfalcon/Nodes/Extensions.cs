using System.Text;

namespace Davfalcon.Nodes
{
	public static class Extensions
	{
		public static string ToStringRecursive(this INode node)
			=> node.ToStringRecursive("");

		public static string ToStringRecursive(this INode node, string decorator)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(decorator + node);
			foreach (INode n in node)
			{
				sb.Append(n.ToStringRecursive(decorator + "\t"));
			}
			return sb.ToString();
		}
	}
}
