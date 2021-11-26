namespace Kleene;

public class ReqExpression : Expression
{
    internal class Model : IModel<ReqExpression>
    {
        public IModel<Expression>? Expression { get; set; }

        public ReqExpression Convert()
        {
            if (Expression is null)
                throw new InvalidOperationException();

            return new(Expression.Convert());
        }
    }

    public Expression Expression { get; }

    public ReqExpression(Expression expression)
    {
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var result in Expression.Run(context))
        {
            if (result.Input.Length != 0)
            {
                yield return result;
            }
        }
    }
}
