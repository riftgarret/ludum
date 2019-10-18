namespace Davfalcon.Nodes
{
    public class ValueNode<T> : NodeBase<T> where T : struct
    {
        public ValueNode(T value)
        {
            Value = value;
        }
    }
}
