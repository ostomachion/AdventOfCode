namespace Kleene;

public class CharacterClass
{
    public IEnumerable<char> Characters { get; }
    public bool Negated { get; }

    public CharacterClass(IEnumerable<char> characters, bool negated)
    {
        Characters = characters;
        Negated = negated;
    }

    public bool Accepts(char c) => Negated ^ Characters.Contains(c);
}
