namespace Kleene;

public class CaptureTreeNode
{
    public CaptureTree Tree { get; }
    public string Name { get; }
    public CaptureTreeNode? Parent { get; set; }
    private readonly Stack<CaptureTreeNode> children = new();
    public ExpressionResult? Value { get; set; }
    public IEnumerable<CaptureTreeNode> Children => children;
    public bool IsOpen { get; set; }
    public bool IsFunctionBoundary { get; set; }
    public bool IsPropertyBoundary => Name != "" && Char.IsUpper(Name[0]);

    public CaptureTreeNode? this[CaptureName? name]
    {
        get
        {
            if (name is null)
            {
                return this;
            }

            var head = children.FirstOrDefault(x => x.Name == name.Head);
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
        children.Push(node);
    }

    public void Unadd()
    {
        var node = children.Pop();
        node.Parent = null;
    }

    public T Parse<T>()
    {
        var typeNode = GetTypeAssignmentNode();
        var type = typeNode is null ? typeof(T) : ResolveTypeName(typeNode.Name);

        object value;
        if (type.IsEnum)
        {
            // TODO:Enum.TryParse<C>(ReadOnlySpan<char>, out C) || (C)Int32.Parse(ReadOnlySpan<char>)
            throw new NotImplementedException();
        }
        else if (type.GetInterfaces().Contains(typeof(IEnumerable<char>)))
        {
            // TODO: parse as string
            throw new NotImplementedException();
        }
        else if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
        {
            // TODO: parse each value as type, then construct collection (???)
            throw new NotImplementedException();
        }
        else
        {
            // 5. Otherwise, parse as C
            //      - bool Enum.TryParse<C>(ReadOnlySpan<char>, out C)
            //      - bool TryParse(ReadOnlySpan<char>, out C)
            //      - bool TryParse(string, out C)
            //      - C Parse(ReadOnlySpan<char>)
            //      - C Parse(string)
            //      - bool C.TryParse<T>(ReadOnlySpan<char>, out C)
            //      - bool C.TryParse<T>(string, out C)
            //      - bool C.TryParse(Type, ReadOnlySpan<char>, out C)
            //      - bool C.TryParse(Type, string, out C)
            //      - bool C.TryParse(ReadOnlySpan<char>, out C)
            //      - bool C.TryParse(string, out C)
            //      - C C.Parse<C>(ReadOnlySpan<char>)
            //      - C C.Parse<C>(string)
            //      - C C.Parse(Type, ReadOnlySpan<char>)
            //      - C C.Parse(Type, string)
            //      - C C.Parse(ReadOnlySpan<char>)
            //      - C C.Parse(string)
            //      - implicit C (ReadOnlySpan<char>)
            //      - explicit C (ReadOnlySpan<char>)
            //      - implicit C (string)
            //      - explicit C (string)
            //      - new C()
            throw new NotImplementedException();
        }

        // 6. Parse and set explicit properties

        // 7. Parse and set captured properties

        return (T)value;
    }

    public CaptureTreeNode? GetTypeAssignmentNode()
    {
        if (Name == "!T")
            return this;

        foreach (var node in Children.Where(x => !x.IsFunctionBoundary && !x.IsPropertyBoundary))
        {
            if (node.GetTypeAssignmentNode() is CaptureTreeNode value)
                return value;
        }

        return null;
    }

    private Type ResolveTypeName(string name)
    {
        throw new NotImplementedException();
    }
}
