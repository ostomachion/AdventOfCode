using System;
using System.Linq;

namespace Kleene
{
    public class CaptureTree
    {
        public CaptureTreeNode Root { get; }
        public CaptureTreeNode Current { get; private set; }

        public CaptureTreeNode? this[CaptureName? name]
        {
            get
            {
                if (name is null)
                    return Current;
                
                var head = Current.Children.LastOrDefault(x => x.Name == name.Head);
                if (head is not null)
                    return head[name.Tail];

                return Current.Parent?[name];
            }
        }

        public CaptureTree()
        {
            this.Root = new(this, "root");
            this.Current = Root;
        }

        public void Open(string name)
        {
            var node = new CaptureTreeNode(this, name);
            this.Current.Add(node);
            this.Current = node;
        }

        public void Unopen()
        {
            this.Current = this.Current.Parent ?? throw new InvalidOperationException();
            this.Current.Unadd();
        }

        public void Close(ExpressionResult value)
        {
            this.Current.Value = value;
            this.Current = this.Current.Parent ?? throw new InvalidOperationException();
        }

        public void Unclose()
        {
            this.Current = this.Current.Children.Last();
            this.Current.Value = null;
        }
    }
}
