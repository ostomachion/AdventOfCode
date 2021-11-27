namespace Kleene;

public class ConcatExpression : Expression
{
    public IEnumerable<Expression> Expressions { get; }

    public ConcatExpression(params Expression[] expressions)
    {
        Expressions = expressions;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (!Expressions.Any())
        {
            yield return new();
            yield break;
        }

        var stack = new Stack<IEnumerator<ExpressionResult>>();
        stack.Push(Expressions.First().Run(context).GetEnumerator());

        while (stack.Any())
        {
            if (stack.Peek().MoveNext())
            {
                if (stack.Count == Expressions.Count())
                {
                    yield return new(
                        string.Join("", stack.Reverse().Select(x => x.Current.Input)),
                        string.Join("", stack.Reverse().Select(x => x.Current.Output)));
                }
                else
                {
                    stack.Push(Expressions.ElementAt(stack.Count).Run(context).GetEnumerator());
                }
            }
            else
            {
                stack.Pop();
            }
        }
    }
}
