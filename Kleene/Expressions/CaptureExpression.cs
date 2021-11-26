namespace Kleene;

public class CaptureExpression : Expression
{
    internal class Model : IModel<CaptureExpression>
    {
        public string? Name { get; set; }
        public IModel<Expression>? Expression { get; set; }

        public CaptureExpression Convert()
        {
            if (Name is null || Expression is null)
                throw new InvalidOperationException();

            return new(Name, Expression.Convert());
        }
    }

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
}
