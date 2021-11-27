namespace Kleene.Models;

public class ReqExpressionModel : IModel<ReqExpression>
{
    public IModel<Expression>? Expression { get; set; }

    public ReqExpression Convert()
    {
        if (Expression is null)
            throw new InvalidOperationException();

        return new(Expression.Convert());
    }
}
