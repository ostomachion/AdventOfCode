namespace Kleene;

public class FunctionExpression : Expression
{
    internal class Model : IModel<FunctionExpression>
    {
        public string? Name { get; set; }
        public IModel<Expression>? Expression { get; set; }

        public FunctionExpression Convert()
        {
            if (Name is null || Expression is null)
                throw new InvalidOperationException();

            return new(Name, Expression.Convert());
        }
    }

    public string Name { get; }
    public Expression Expression { get; }

    public FunctionExpression(string name, Expression expression)
    {
        Name = name;
        Expression = expression;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        context.DefineFunction(Name, Expression);
        yield return new();
        context.UndefineFunction(Name);
    }
}
