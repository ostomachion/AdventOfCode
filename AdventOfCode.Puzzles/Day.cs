namespace AdventOfCode.Puzzles;

public abstract class Day
{
    private string input = null!;
    public string Input
    {
        get => input;
        set => input = value.ReplaceLineEndings("\n");
    }

    public abstract Output Part1();
    public abstract Output Part2();

    public static Output AnswerNotFound() => throw new AnswerNotFoundException();
}
