namespace Kleene;

public class SubExpression : Expression
{
    internal class Model : IModel<SubExpression>
    {
        public IModel<TextValueExpression>? Input { get; set; }
        public IModel<Expression>? Expression { get; set; }

        public SubExpression Convert()
        {
            if (Input is null || Expression is null)
                throw new InvalidOperationException();

            return new(Input.Convert(), Expression.Convert());
        }
    }

    public TextValueExpression Input { get; }
    public Expression? Expression { get; }

    public SubExpression(TextValueExpression input, Expression? expression = null)
    {
        if (input is TextExpression && expression is null)
        {
            throw new ArgumentException("A sub-expression on a text expression must have an expression to match against.", nameof(input));
        }

        Input = input;
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var sub = Input.GetValue(context);
        if (sub is null)
        {
            yield break;
        }

        // If no expression is given, pass if the input value exists.
        if (Expression is null)
        {
            yield return new();
            yield break;
        }

        var originalContext = context.Local;
        var subContext = new ExpressionLocalContext(sub.Input);

        context.Local = subContext;
        foreach (var _ in Expression.Run(context))
        {
            context.Local = originalContext;
            yield return new();
            context.Local = subContext;
        }
        context.Local = originalContext;
    }
}
