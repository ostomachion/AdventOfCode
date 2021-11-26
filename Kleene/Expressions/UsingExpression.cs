namespace Kleene;

public class UsingExpression : Expression
{
    internal class Model : IModel<UsingExpression>
    {
        public string? NamespaceName { get; set; }

        public UsingExpression Convert()
        {
            if (NamespaceName is null)
                throw new InvalidOperationException();

            return new(NamespaceName);
        }
    }

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
}
