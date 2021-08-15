using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class CaptureTreeNode
    {
        public CaptureTree Tree { get; }
        public string Name { get; }
        public CaptureTreeNode? Parent { get; set; }
        private readonly Stack<CaptureTreeNode> children = new();
        public ExpressionResult? Value { get; set; }
        public IEnumerable<CaptureTreeNode> Children => this.children;
        public bool IsOpen { get; set; }
        public bool IsFunctionBoundary { get; set; }

        public CaptureTreeNode? this[CaptureName? name]
        {
            get
            {
                if (name is null)
                    return this;

                var head = this.children.FirstOrDefault(x => x.Name == name.Head);
                return head?[name.Tail];
            }
        }

        public CaptureTreeNode(CaptureTree tree, string name)
        {
            Tree = tree;
            Name = name;
        }

        public void Add(CaptureTreeNode node)
        {
            node.Parent = this;
            this.children.Push(node);
        }

        public void Unadd()
        {
            var node = this.children.Pop();
            node.Parent = null;
        }
    }
}
