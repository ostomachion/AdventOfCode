namespace Kleene;

public class PassExpression : Expression
{
    internal class Model : IModel<PassExpression>
    {
        public PassExpression Convert() => new();
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        yield return new();
    }
}
