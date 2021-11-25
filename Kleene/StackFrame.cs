namespace Kleene;

public class StackFrame
{
    public string Name { get; }
    public Expression Expression { get; }
    public bool Ratchet { get; set; }

    public StackFrame(string name, Expression expression)
    {
        Name = name;
        Expression = expression;
    }
}

