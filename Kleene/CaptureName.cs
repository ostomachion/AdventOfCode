using System.Diagnostics.CodeAnalysis;

namespace Kleene;

public class CaptureName
{
    public IEnumerable<string> Parts { get; }
    public string Head => Parts.First();
    public CaptureName? Tail => Parts.Count() == 1 ? null : new(Parts.Skip(1).ToArray());

    public CaptureName(string dottedName) : this(dottedName.Split('.')) { }

    public CaptureName(params string[] parts)
    {
        if (!parts.Any())
        {
            throw new ArgumentException("CaptureName cannot be empty.", nameof(parts));
        }

        Parts = parts;
    }


    [return: NotNullIfNotNull("value")]

    public static implicit operator CaptureName?(string? value) => value is null ? null : new(value);

    public override string ToString() => string.Join('.', Parts);
}
