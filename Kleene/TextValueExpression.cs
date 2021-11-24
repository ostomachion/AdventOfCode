namespace Kleene;

public abstract class TextValueExpression : Expression
{
    public abstract ExpressionResult? GetValue(ExpressionContext context);

    public static implicit operator TextValueExpression(string value) => new TextExpression(value);
}
