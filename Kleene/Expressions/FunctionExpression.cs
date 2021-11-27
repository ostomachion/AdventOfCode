namespace Kleene;

public class FunctionExpression : Expression
{
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
