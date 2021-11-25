using System;
using System.Linq;

namespace Kleene
{
    public class CaptureTree
    {
        public CaptureTreeNode Root { get; }
        public CaptureTreeNode Current { get; set; }

        public CaptureTreeNode? this[CaptureName? name]
        {
            get
            {
                if (name is null)
                    return Current;
                
                var head = Current.Children.FirstOrDefault(x => x.Name == name.Head);
                if (head is not null)
                    return head[name.Tail];

                return (Current.IsFunctionBoundary || Current.Parent is null) ? null : Current.Parent[name];
            }
        }

        public CaptureTree()
        {
            this.Root = new(this, "root");
            this.Current = Root;
        }

        public void Set(CaptureName name, ExpressionResult value)
        {
            Open(name);
            Close(name, value);
        }

        public void Unset(CaptureName name)
        {
            Unclose(name);
            Unopen(name);
        }

        public void Open(CaptureName name)
        {
            foreach (var part in name.Parts)
            {
                var node = new CaptureTreeNode(this, part);
                this.Current.Add(node);
                this.Current = node;
            }
        }

        public void Unopen(CaptureName name)
        {
            foreach (var part in name.Parts.Reverse())
            {
                if (this.Current.Name != part)
                    throw new InvalidOperationException();
                
                this.Current = this.Current.Parent ?? throw new InvalidOperationException();
                this.Current.Unadd();
            }
        }

        public void Close(CaptureName name, ExpressionResult value)
        {
            foreach (var part in name.Parts.Reverse())
            {
                if (this.Current.Name != part)
                    throw new InvalidOperationException();
                
                this.Current.IsOpen = false;
                this.Current.Value = value;
                this.Current = this.Current.Parent ?? throw new InvalidOperationException();
            }
        }

        public void Unclose(CaptureName name)
        {
            foreach (var part in name.Parts.Reverse())
            {   
                this.Current = this.Current.Children.First();
                if (this.Current.Name != part)
                    throw new InvalidOperationException();

                this.Current.Value = null;
                this.Current.IsOpen = true;
            }
        }
    }
}
