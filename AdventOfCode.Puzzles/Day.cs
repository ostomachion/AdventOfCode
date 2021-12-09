namespace AdventOfCode.Puzzles;

public abstract class Day
{
    private string input = null!;
    public string Input
    {
        get => input;
        set => input = value.ReplaceLineEndings("\n");
    }

    public virtual string? TestInput { get; }
    public virtual Output? TestOutputPart1 { get; }
    public virtual Output? TestOutputPart2 { get; }

    public abstract Output Part1();
    public abstract Output Part2();

    public static Output AnswerNotFound() => "Answer not found.";
}
