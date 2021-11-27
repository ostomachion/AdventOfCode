namespace Kleene;

public class Anchor : IEquatable<Anchor?>
{
    public static readonly Anchor StartOfLine = new(AnchorType.Start, new("\n", true), false);
    public static readonly Anchor EndOfLine = new(AnchorType.End, new("\n", true), false);
    public static readonly Anchor StartOfText = new(AnchorType.Start, CharacterClass.Any, false);
    public static readonly Anchor EndOfText = new(AnchorType.End, CharacterClass.Any, false);

    public AnchorType Type { get; }
    public CharacterClass CharacterClass { get; }
    public bool Negated { get; }

    public Anchor(AnchorType type, CharacterClass characterClass, bool negated)
    {
        Type = type;
        CharacterClass = characterClass;
        Negated = negated;
    }
    public override string ToString()
    {
        if (this == StartOfLine)
        {
            return "^^";
        }
        else if (this == EndOfLine)
        {
            return "$$";
        }
        else if (this == StartOfText)
        {
            return "^";
        }
        else if (this == EndOfText)
        {
            return "$";
        }
        else
        {
            string value = "";

            value += Type == AnchorType.Start || Type == AnchorType.Outer ? "<" : ">";
            
            if (Negated)
                value += "!";

            if (CharacterClass != CharacterClass.Word)
                value += CharacterClass.ToString();

            value += Type == AnchorType.Start || Type == AnchorType.Inner ? "<" : ">";

            return value;
        }
    }

    public override bool Equals(object? obj) => Equals(obj as Anchor);

    public bool Equals(Anchor? other)
    {
        return other != null &&
               Type == other.Type &&
               EqualityComparer<CharacterClass>.Default.Equals(CharacterClass, other.CharacterClass) &&
               Negated == other.Negated;
    }

    public override int GetHashCode() => HashCode.Combine(Type, CharacterClass, Negated);

    public static bool operator ==(Anchor? left, Anchor? right)
    {
        return EqualityComparer<Anchor>.Default.Equals(left, right);
    }

    public static bool operator !=(Anchor? left, Anchor? right)
    {
        return !(left == right);
    }
}
