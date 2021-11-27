namespace Kleene;

public class CharacterClass : IEquatable<CharacterClass?>
{
    public static readonly CharacterClass HorizontalSpace = new(" \t", false);
    public static readonly CharacterClass Space = new(" \t\n", false);
    public static readonly CharacterClass Digit = new("0123456789", false);
    public static readonly CharacterClass Upper = new("ABCDEFGHIJKLMNOPQRSTUVWXYZ", false);
    public static readonly CharacterClass Lower = new("abcdefghijklmnopqrstuvwxyz", false);
    public static readonly CharacterClass Alpha = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", false);
    public static readonly CharacterClass Word = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", false);

    public static readonly CharacterClass NotHorizontalSpace = new(" \t", true);
    public static readonly CharacterClass NotSpace = new(" \t\n", true);
    public static readonly CharacterClass NotDigit = new("0123456789", true);
    public static readonly CharacterClass NotUpper = new("ABCDEFGHIJKLMNOPQRSTUVWXYZ", true);
    public static readonly CharacterClass NotLower = new("abcdefghijklmnopqrstuvwxyz", true);
    public static readonly CharacterClass NotAlpha = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);
    public static readonly CharacterClass NotWord = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", true);

    public static readonly CharacterClass Any = new("", true);


    private readonly HashSet<char> characters;
    public IEnumerable<char> Characters => characters;
    public bool Negated { get; }

    public CharacterClass(IEnumerable<char> characters, bool negated)
    {
        this.characters = characters.ToHashSet();
        Negated = negated;
    }

    public bool Accepts(char c) => Negated ^ Characters.Contains(c);

    public override string ToString()
    {
        if (!Negated && characters.SetEquals("\\"))
        {
            return @"\\";
        }
        else if (!Negated && characters.SetEquals("\n"))
        {
            return @"\n";
        }
        else if (!Negated && characters.SetEquals("\t"))
        {
            return @"\t";
        }
        else if (this == HorizontalSpace)
        {
            return @"\h";
        }
        else if (this == Space)
        {
            return @"\s";
        }
        else if (this == Digit)
        {
            return @"\d";
        }
        else if (this == Upper)
        {
            return @"\u";
        }
        else if (this == Lower)
        {
            return @"\l";
        }
        else if (this == Alpha)
        {
            return @"\a";
        }
        else if (this == Word)
        {
            return @"\w";
        }
        else if (this == Any)
        {
            return @".";
        }
        else if (Negated && characters.SetEquals("\n"))
        {
            return @"\N";
        }
        else if (Negated && characters.SetEquals("\t"))
        {
            return @"\T";
        }
        else if (this == NotHorizontalSpace)
        {
            return @"\H";
        }
        else if (this == NotSpace)
        {
            return @"\S";
        }
        else if (this == NotDigit)
        {
            return @"\D";
        }
        else if (this == NotUpper)
        {
            return @"\U";
        }
        else if (this == NotLower)
        {
            return @"\L";
        }
        else if (this ==  NotAlpha)
        {
            return @"\A";
        }
        else if (this == NotWord)
        {
            return @"\W";
        }
        else
        {
            var value = "[";
            if (Negated)
                value += "^";

            var c = characters.ToHashSet();
            if (c.IsSupersetOf(Word.characters))
            {
                value += @"\w";
                c.RemoveWhere(Word.characters.Contains);
            }

            if (c.IsSupersetOf(Alpha.characters))
            {
                value += @"\a";
                c.RemoveWhere(Alpha.characters.Contains);
            }

            if (c.IsSupersetOf(Upper.characters))
            {
                value += @"\u";
                c.RemoveWhere(Upper.characters.Contains);
            }

            if (c.IsSupersetOf(Lower.characters))
            {
                value += @"\l";
                c.RemoveWhere(Lower.characters.Contains);
            }

            if (c.IsSupersetOf(Digit.characters))
            {
                value += @"\d";
                c.RemoveWhere(Digit.characters.Contains);
            }

            if (c.IsSupersetOf(Space.characters))
            {
                value += @"\s";
                c.RemoveWhere(Space.characters.Contains);
            }

            if (c.IsSupersetOf(HorizontalSpace.characters))
            {
                value += @"\h";
                c.RemoveWhere(Alpha.characters.Contains);
            }

            if (c.Contains('['))
            {
                value += @"[<]";
                c.Remove('[');
            }

            if (c.Contains(']'))
            {
                value += @"[>]";
                c.Remove(']');
            }

            if (c.Contains('\\'))
            {
                value += @"\\";
                c.Remove('\\');
            }

            if (c.Contains('n'))
            {
                value += @"\n";
                c.Remove('\n');
            }

            if (c.Contains('\t'))
            {
                value += @"\t";
                c.Remove('\t');
            }

            value += new string(c.ToArray());

            value += "]";
            return value;
        }
    }

    public override bool Equals(object? obj) => Equals(obj as CharacterClass);

    public bool Equals(CharacterClass? other)
    {
        return other is not null &&
            Negated == other.Negated &&
            characters.SetEquals(other.characters);
    }

    public override int GetHashCode() => HashCode.Combine(characters, Characters, Negated);

    public static bool operator ==(CharacterClass? left, CharacterClass? right)
    {
        return EqualityComparer<CharacterClass>.Default.Equals(left, right);
    }

    public static bool operator !=(CharacterClass? left, CharacterClass? right)
    {
        return !(left == right);
    }
}
