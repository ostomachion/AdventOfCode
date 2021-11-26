namespace Kleene;

public class TextExpression : TextValueExpression
{
    internal class Model : IModel<TextExpression>
    {
        public string? Value { get; set; }

        public TextExpression Convert()
        {
            if (Value is null)
                throw new InvalidOperationException();

            return new(Value);
        }

        public static implicit operator Model(string value) => new() { Value = value };
    }

    public string Value { get; }

    public TextExpression(string value)
    {
        Value = value;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var remaining = context.Local.Input.AsSpan()[context.Local.Index..];
        if (!context.Local.Consuming || remaining.StartsWith(Value))
        {
            context.Consume(Value.Length);
            context.Produce(Value);
            yield return new(Value);
            context.Unproduce();
            context.Unconsume(Value.Length);
        }
    }

    public override ExpressionResult? GetValue(ExpressionContext context) => new(Value);
}
