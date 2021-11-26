namespace Kleene;

public class ScopeExpression : Expression
{
    internal class Model : IModel<ScopeExpression>
    {
        public string? Name { get; set; }
        public IModel<Expression>? Expression { get; set; }

        public ScopeExpression Convert()
        {
            if (Name is null || Expression is null)
                throw new InvalidOperationException();

            return new(Name, Expression.Convert());
        }
    }

    public CaptureName Name { get; }
    public Expression Expression { get; }

    public ScopeExpression(CaptureName name, Expression expression)
    {
        Name = name;
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var originalScope = context.CaptureTree.Current;
        var newScope = context.CaptureTree[Name].FirstOrDefault();
        if (newScope is null)
        {
            yield break;
        }

        context.CaptureTree.Current = newScope;
        foreach (var result in Expression.Run(context))
        {
            context.CaptureTree.Current = originalScope;
            yield return result;
            context.CaptureTree.Current = newScope;
        }
        context.CaptureTree.Current = originalScope;
    }
}
