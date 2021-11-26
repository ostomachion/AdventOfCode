namespace Kleene;

public class RenameExpression : Expression
{
    internal class Model : IModel<RenameExpression>
    {
        public string? Name { get; set; }
        public string? NewName { get; set; }

        public RenameExpression Convert()
        {
            if (Name is null || NewName is null)
                throw new InvalidOperationException();

            return new(Name, NewName);
        }
    }

    public CaptureName Name { get; }
    public CaptureName NewName { get; }

    public RenameExpression(CaptureName name, CaptureName newName)
    {
        if (newName.Parts.Count() != 1)
            throw new NotImplementedException(); // TODO:
        Name = name;
        NewName = newName;
    }

    public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
    {
        foreach (var capture in context.CaptureTree[Name])
        {
            capture.Name = NewName.Head;
        }

        yield return new();

        var localName = Name.Parts.Last();
        var newFullName = new CaptureName(Name.Parts.Take(Name.Parts.Count() - 1).Concat(NewName.Parts).ToArray());
        foreach (var capture in context.CaptureTree[newFullName])
        {
            capture.Name = localName;
        }
    }
}
