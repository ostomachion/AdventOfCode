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

    public override string ToString()
    {
        var value = "";
        var lineLength = 0;
        var startOfLine = true;
        foreach (var p in Expressions.Select((x, i) => (x, i)))
        {
            var expression = p.x;
            var text = expression.ToString()!;

            if (expression is ConcatExpression or AltExpression or TransformExpression)
            {
                if (text.Contains('\n'))
                {
                    text = "(\n  " + text.Replace("\n", "\n  ") + "\n)";
                }
                else
                {
                    text = $"({text})";
                }
            }
            else if (expression is FunctionExpression or UsingExpression)
            {
                if (value != "" && !value.EndsWith("\n"))
                    text = "\n" + text;
                text += "\n";
            }

            if (text.Contains('\n'))
            {
                var lastLine = text.Split('\n')[^1];
                if (p.i != Expressions.Count() - 1 && Expressions.ElementAt(p.i + 1) is RatchetExpression && lastLine.Length + 2 < ToStringLength)
                {
                    if (!startOfLine)
                        value += "\n";
                    value += text;
                    lineLength = lastLine.Length;
                    startOfLine = lastLine == "";
                }
                else
                {
                    if (value != "" && !value.EndsWith("\n"))
                        text = "\n" + text;
                    text += "\n";
                    value += text;
                    lineLength = 0;
                    startOfLine = true;
                }
            }
            else if (lineLength + text.Length + (startOfLine ? 0 : 1) <= ToStringLength)
            {
                if (!startOfLine)
                    value += " ";
                value += text;
                lineLength += text.Length;
                startOfLine = false;

                if (expression is RatchetExpression && p.i != Expressions.Count() - 1)
                {
                    value += "\n";
                    lineLength = 0;
                    startOfLine = true;
                }
            }
            else
            {
                if (!startOfLine)
                    value += "\n";
                value += text;
                lineLength = text.Length;
                startOfLine = false;
            }
        }

        return value;
    }
}
