namespace Kleene;

public class CaptureExpression : Expression
{
    public CaptureName Name { get; }
    public Expression Expression { get; }

    public CaptureExpression(CaptureName name, Expression expression)
    {
        Name = name;
        Expression = expression; 
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.CaptureTree.Open(Name);
        foreach (var result in Expression.Run(context))
        {
            context.CaptureTree.Close(Name, result);
            yield return result;
            context.CaptureTree.Unclose(Name);
        }
        context.CaptureTree.Unopen(Name);
    }

    public override string ToString()
    {
        var text = Expression.ToString()!;
        if (Expression is ConcatExpression or AltExpression or TransformExpression)
        {
            if (text.Contains('\n'))
            {
                text = "(\n  " + text.Replace("\n", "\n  ") + "\n)";
            }
            else
            {
                text = $"({text})";
            }
        }
        return $"{text}:{Name}";
    }
}
