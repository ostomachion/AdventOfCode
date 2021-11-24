namespace Kleene;

public class ScopeExpression : Expression
{
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
        var newScope = context.CaptureTree.Current[Name];
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
