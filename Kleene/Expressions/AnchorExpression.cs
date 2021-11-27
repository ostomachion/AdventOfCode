namespace Kleene;

public class AnchorExpression : Expression
{
    internal class Model : IModel<AnchorExpression>
    {
        public AnchorType? Type { get; set; }
        public CharacterClass.Model? CharacterClass { get; set; }
        public bool Negated { get; set; }

        public AnchorExpression Convert()
        {
            if (Type is null || CharacterClass is null)
                throw new InvalidOperationException();

            return new(Type.Value, CharacterClass.Convert(), Negated);
        }
    }

    public AnchorType Type { get; }
    public CharacterClass CharacterClass { get; }
    public bool Negated { get; }

    public AnchorExpression(AnchorType type, CharacterClass characterClass, bool negated)
    {
        Type = type;
        CharacterClass = characterClass;
        Negated = negated;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        var prev = context.Local.IsAtStart ? (char?)null : context.Local.Input[context.Local.Index - 1];
        var next = context.Local.IsAtEnd ? (char?)null : context.Local.Input[context.Local.Index];

        var prevMatches = prev is not null && CharacterClass.Accepts(prev.Value);
        var nextMatches = next is not null && CharacterClass.Accepts(next.Value);

        var pass = Type switch
        {
            AnchorType.Left => nextMatches && !prevMatches,
            AnchorType.Right => prevMatches && !nextMatches,
            AnchorType.Outer => prevMatches ^ nextMatches,
            AnchorType.Inner => prevMatches && nextMatches,
            _ => throw new InvalidOperationException()
        };

        if (pass ^ Negated)
        {
            yield return new();
        }
    }
}
