namespace Kleene;

public class CharacterClass
{
    internal class Model : IModel<CharacterClass>
    {
        public List<char>? Characters { get; set; }
        public bool? Negated { get; set; }

        public CharacterClass Convert()
        {
            if (Characters is null || Negated is null)
                throw new InvalidOperationException();

            return new(Characters, Negated.Value);
        }
    }

    public IEnumerable<char> Characters { get; }
    public bool Negated { get; }

    public CharacterClass(IEnumerable<char> characters, bool negated)
    {
        Characters = characters;
        Negated = negated;
    }

    public bool Accepts(char c) => Negated ^ Characters.Contains(c);
}
