using System.Collections.Generic;

namespace Davfalcon.Nodes
{
    public interface INode<T> : IEnumerable<INode<T>>
    {
        T Value { get; }
    }
}
