using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class RepExpression : Expression
    {
        public Expression Expression { get; }
        public RepCount Count { get; }
        public MatchOrder Order { get; }

        public RepExpression(Expression expression, RepCount count, MatchOrder order = MatchOrder.Greedy)
        {
            Expression = expression;
            Count = count;
            Order = order;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            if (Order == MatchOrder.Lazy && Count.Min == 0 || Count.Max == 0)
            {
                yield return new ExpressionResult("");
                if (Count.Max == 0)
                {
                    yield break;
                }
            }

            var stack = new Stack<IEnumerator<ExpressionResult>>();
            stack.Push(this.Expression.Run(context).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == Count.Max)
                    {
                        yield return new ExpressionResult(
                            String.Join("", stack.Reverse().Select(x => x.Current.Input)),
                            String.Join("", stack.Reverse().Select(x => x.Current.Output)));
                    }
                    else
                    {
                        if (Order == MatchOrder.Lazy && stack.Count >= Count.Min)
                        {
                            yield return new ExpressionResult(
                                String.Join("", stack.Reverse().Select(x => x.Current.Input)),
                                String.Join("", stack.Reverse().Select(x => x.Current.Output)));
                        }
                        stack.Push(this.Expression.Run(context).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();

                    if (Order == MatchOrder.Greedy)
                    {
                        if (stack.Any() && stack.Count >= Count.Min || Count.Min == 0)
                        {
                            yield return new ExpressionResult(
                                String.Join("", stack.Reverse().Select(x => x.Current.Input)),
                                String.Join("", stack.Reverse().Select(x => x.Current.Output)));
                        }
                    }
                }
            }
        }
    }
}
