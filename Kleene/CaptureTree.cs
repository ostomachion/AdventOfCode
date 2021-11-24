using System;
using System.Linq;

namespace Kleene;

public class CaptureTree
{
    public CaptureTreeNode Root { get; }
    public CaptureTreeNode Current { get; set; }

    public CaptureTreeNode? this[CaptureName? name]
    {
        get
        {
            if (name is null)
            {
                return Current;
            }

            var head = Current.Children.FirstOrDefault(x => x.Name == name.Head);
            if (head is not null)
            {
                return head[name.Tail];
            }

            return (Current.IsFunctionBoundary || Current.Parent is null) ? null : Current.Parent[name];
        }
    }

    public CaptureTree()
    {
        Root = new(this, "root");
        Current = Root;
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
            Current.Add(node);
            Current = node;
        }
    }

    public void Unopen(CaptureName name)
    {
        foreach (var part in name.Parts.Reverse())
        {
            if (Current.Name != part)
            {
                throw new InvalidOperationException();
            }

            Current = Current.Parent ?? throw new InvalidOperationException();
            Current.Unadd();
        }
    }

    public void Close(CaptureName name, ExpressionResult value)
    {
        foreach (var part in name.Parts.Reverse())
        {
            if (Current.Name != part)
            {
                throw new InvalidOperationException();
            }

            Current.IsOpen = false;
            Current.Value = value;
            Current = Current.Parent ?? throw new InvalidOperationException();
        }
    }

    public void Unclose(CaptureName name)
    {
        foreach (var part in name.Parts.Reverse())
        {
            Current = Current.Children.Last();
            if (Current.Name != part)
            {
                throw new InvalidOperationException();
            }

            Current.Value = null;
            Current.IsOpen = true;
        }
    }
}
