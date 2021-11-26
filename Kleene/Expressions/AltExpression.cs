namespace Kleene;

public class AltExpression : Expression
{
    internal class Model : IModel<AltExpression>
    {
        public List<IModel<Expression>>? Expressions { get; set; }

        public AltExpression Convert()
        {
            if (Expressions is null)
                throw new InvalidOperationException();
            
            return new(Expressions.Select(x => x.Convert()).ToArray());
        }
    }

    public IEnumerable<Expression> Expressions { get; }

    public AltExpression(params Expression[] expressions)
    {
        Expressions = expressions;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var expression in Expressions)
        {
            foreach (var result in expression.Run(context))
            {
                yield return result;
            }
        }
    }
}
