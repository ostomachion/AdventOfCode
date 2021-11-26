namespace Kleene;

public class RatchetExpression : Expression
{
    internal class Model : IModel<RatchetExpression>
    {
        public RatchetExpression Convert() => new();
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        yield return new();
        context.Ratchet = true;
    }
}
