namespace Kleene;

public class AnchorExpression : Expression
{
    public Anchor Anchor { get; }

    public AnchorExpression(Anchor anchor)
    {
        Anchor = anchor;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var prev = context.Local.IsAtStart ? (char?)null : context.Local.Input[context.Local.Index - 1];
        var next = context.Local.IsAtEnd ? (char?)null : context.Local.Input[context.Local.Index];

        var prevMatches = prev is not null && Anchor.CharacterClass.Accepts(prev.Value);
        var nextMatches = next is not null && Anchor.CharacterClass.Accepts(next.Value);

        var pass = Anchor.Type switch
        {
            AnchorType.Start => nextMatches && !prevMatches,
            AnchorType.End => prevMatches && !nextMatches,
            AnchorType.Outer => prevMatches ^ nextMatches,
            AnchorType.Inner => prevMatches && nextMatches,
            _ => throw new InvalidOperationException()
        };

        if (pass ^ Anchor.Negated)
        {
            yield return new();
        }
    }

    public override string ToString() => Anchor.ToString();
}
