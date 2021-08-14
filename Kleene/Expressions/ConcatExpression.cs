using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConcatExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }

        public ConcatExpression(IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
        {
            if (!this.Expressions.Any())
            {
                yield return new();
                yield break;
            }

            var stack = new Stack<IEnumerator<ExpressionResult>>();
            stack.Push(this.Expressions.First().Run(context).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return new(
                            String.Join("", stack.Reverse().Select(x => x.Current.Input)),
                            String.Join("", stack.Reverse().Select(x => x.Current.Output)));
                    }
                    else
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(context).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();
                }
            }
        }
    }
}
