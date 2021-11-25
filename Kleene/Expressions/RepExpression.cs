namespace Kleene;

public class RepExpression : Expression
{
    public Expression Expression { get; }
    public Expression? Separator { get; }
    public RepCount Count { get; }
    public MatchOrder Order { get; }

    public RepExpression(Expression expression, Expression? separator, RepCount count, MatchOrder order = MatchOrder.Greedy)
    {
        Expression = expression;
        Separator = separator;
        Count = count;
        Order = order;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        if (!context.Local.Consuming && Count.IsUnbounded)
        {
            throw new InvalidOperationException("Unbounded repetitions cannot be used without an input.");
        }

            var separated = Separator is null ? Expression : new ConcatExpression(Separator, Expression);

        if (Order == MatchOrder.Lazy && Count.Min == 0 || Count.Max == 0)
        {
            yield return new();
            if (Count.Max == 0)
            {
                yield break;
            }
        }

        var stack = new Stack<IEnumerator<ExpressionResult>>();
        stack.Push(Expression.Run(context).GetEnumerator());

        while (stack.Any())
        {
            if (stack.Peek().MoveNext())
            {
                if (stack.Count == Count.Max)
                {
                    yield return new(
                        string.Join("", stack.Reverse().Select(x => x.Current.Input)),
                        string.Join("", stack.Reverse().Select(x => x.Current.Output)));
                }
                else
                {
                    if (Order == MatchOrder.Lazy && stack.Count >= Count.Min)
                    {
                        yield return new(
                            string.Join("", stack.Reverse().Select(x => x.Current.Input)),
                            string.Join("", stack.Reverse().Select(x => x.Current.Output)));
                    }
                    stack.Push(separated.Run(context).GetEnumerator());
                }
            }
            else
            {
                stack.Pop();

                if (Order == MatchOrder.Greedy)
                {
                    if (stack.Any() && stack.Count >= Count.Min || Count.Min == 0)
                    {
                        yield return new(
                            string.Join("", stack.Reverse().Select(x => x.Current.Input)),
                            string.Join("", stack.Reverse().Select(x => x.Current.Output)));
                    }
                }
            }
        }
    }
}
