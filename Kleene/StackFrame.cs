namespace Kleene;

public class StackFrame
{
    public string Name { get; }
    public bool Ratchet { get; set; }
    public List<string> Usings { get; } = new();
    public FunctionList Functions { get; } = new();

    public StackFrame(string name)
    {
        Name = name;
    }
}

