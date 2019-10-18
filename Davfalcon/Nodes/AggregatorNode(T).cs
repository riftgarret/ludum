using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
    public class AggregatorNode<T> : NodeBase<T>
    {
        private readonly Func<T, T, T> func;

        public AggregatorNode(IEnumerable<INode<T>> nodes, Func<T, T, T> func)
            : this(nodes, func, default)
        {
        }

        public AggregatorNode(IEnumerable<INode<T>> nodes, Func<T, T, T> func, T seed)
        {
            this.func = func ?? throw new ArgumentNullException();
            if (nodes == null) throw new ArgumentNullException();

            Nodes = nodes.ToList();
            Value = nodes
                .Select(node => node.Value)
                .Aggregate(seed, func);
        }
    }
}
