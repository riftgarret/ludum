using System;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public abstract class NodeEnumerableBase : INode, IEnumerable<INode>
	{
		public virtual int Value { get; protected set; }
		public virtual string Name { get; protected set; }

		private class DummyEnumerator : IEnumerator<INode>
		{
			INode IEnumerator<INode>.Current => null;
			object IEnumerator.Current => null;
			void IDisposable.Dispose() { }
			bool IEnumerator.MoveNext() => false;
			void IEnumerator.Reset() { }
		}

		protected virtual string GetTypeName() => "Node";

		protected virtual IEnumerator<INode> GetEnumerator()
			=> new DummyEnumerator();

		IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
			=> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		public override string ToString()
			=> $"{Value} : [{GetTypeName()}] {Name}";
	}
}
