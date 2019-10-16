using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
    public abstract class NodeBase<T> : INode<T>, IEnumerable<INode<T>>
    {
        public virtual T Value { get; protected set; }

        protected IEnumerable<INode<T>> Nodes { get; set; } = Enumerable.Empty<INode<T>>();

        IEnumerator<INode<T>> IEnumerable<INode<T>>.GetEnumerator()
            => Nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => Nodes.GetEnumerator();

        public override string ToString()
            => $"{Value} [{GetType().Name}]";
    }
}
