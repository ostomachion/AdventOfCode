namespace Kleene.Models;

public class SubExpressionModel : IModel<SubExpression>
{
    public IModel<TextValueExpression>? Input { get; set; }
    public IModel<Expression>? Expression { get; set; }

    public SubExpression Convert()
    {
        if (Input is null)
            throw new InvalidOperationException();

        return Expression is not null ? new(Input.Convert(), Expression.Convert())
            : new(Input.Convert());
    }
}
