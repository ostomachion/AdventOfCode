namespace Kleene;

public class CaptureTree
{
    public const string RootCaptureName = "root";

    public CaptureTreeNode Root { get; }
    public CaptureTreeNode? Current { get; set; }

    public IEnumerable<CaptureTreeNode> this[CaptureName? name]
    {
        get
        {
            if (name is null)
            {
                return Current is null ? Enumerable.Empty<CaptureTreeNode>() : new[] { Current };
            }

            IEnumerable<CaptureTreeNode> head;
            if (Current is null)
            {
                head = name.Head == RootCaptureName ? new [] { Root } : Enumerable.Empty<CaptureTreeNode>();
            }
            else
            {
                head = Current?.Children.Where(x => x.Name == name.Head) ?? Enumerable.Empty<CaptureTreeNode>();
            }

            var value = head.SelectMany(x => x[name.Tail]);

            return (Current is null || Current.IsFunctionBoundary || Current.Parent is null) ? value : value.Concat(Current.Parent[name]);
        }
    }

    public CaptureTree()
    {
        Root = new(this, RootCaptureName);
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
        if (Current is null)
            throw new InvalidOperationException("Capture tree is fully closed.");

        foreach (var part in name.Parts)
        {
            var node = new CaptureTreeNode(this, part);
            Current.Add(node);
            Current = node;
        }
    }

    public void Unopen(CaptureName name)
    {
        if (Current is null)
            throw new InvalidOperationException("Capture tree is fully closed.");

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
        if (Current is null)
            throw new InvalidOperationException("Capture tree is fully closed.");

        foreach (var part in name.Parts.Reverse())
        {
            if (Current?.Name != part)
            {
                throw new InvalidOperationException();
            }

            Current.IsOpen = false;
            Current.Value = value;
            Current = Current.Parent;
        }
    }

    public void Unclose(CaptureName name)
    {
        if (Current is null)
            throw new InvalidOperationException("Capture tree is fully closed.");

        foreach (var part in name.Parts.Reverse())
        {   
            this.Current = this.Current.Children.First();
            if (this.Current.Name != part)
                throw new InvalidOperationException();

            Current.Value = null;
            Current.IsOpen = true;
        }
    }
}
