namespace Kleene.Models;

public class TextExpressionModel : IModel<TextExpression>
{
    public string? Value { get; set; }

    public TextExpression Convert()
    {
        if (Value is null)
            throw new InvalidOperationException();

        return new(Value);
    }

    public static implicit operator TextExpressionModel(string value) => new() { Value = value };
}
