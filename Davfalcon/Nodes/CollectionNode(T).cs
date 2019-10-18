using System.Linq;

namespace Davfalcon.Nodes
{
    public class CollectionNode<T> : NodeBase<T>
    {
        public CollectionNode(params INode<T>[] nodes)
        {
            Nodes = nodes.ToList();
        }
    }
}
