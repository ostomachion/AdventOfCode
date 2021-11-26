namespace Kleene;

public class FailExpression : Expression
{
    internal class Model : IModel<FailExpression>
    {
        public FailExpression Convert() => new();
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        yield break;
    }
}
