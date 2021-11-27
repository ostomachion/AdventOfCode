namespace Kleene;

public class UsingExpression : Expression
{
    public string NamespaceName { get; }

    public UsingExpression(string namespaceName)
    {
        NamespaceName = namespaceName;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.CallStack.Peek().Usings.Add(NamespaceName);
        yield return new();
        context.CallStack.Peek().Usings.Remove(NamespaceName);
    }

    public override string ToString()
    {
        return $":::{NamespaceName}";
    }
}
