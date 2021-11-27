namespace Kleene.Models;

public class TransformExpressionModel : IModel<TransformExpression>
{
    public IModel<Expression>? Input { get; set; }
    public IModel<Expression>? Output { get; set; }

    public TransformExpression Convert()
    {
        if (Input is null || Output is null)
            throw new InvalidOperationException();

        return new(Input.Convert(), Output.Convert());
    }
}
