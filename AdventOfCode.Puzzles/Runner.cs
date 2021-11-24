namespace AdventOfCode.Puzzles;

public class Runner
{
    private readonly Client client;
    private readonly InputManager inputManager;

    public Runner()
    {
        client = new();
        inputManager = new(client);
    }

    public static (int year, int day) GetCurrentDecemberDate()
    {
        var date = DateTimeOffset.Now.ToOffset(new TimeSpan(-5, 0, 0)).Date;
        if (date.Month != 12)
        {
            throw new InvalidOperationException("The current date is not in December.");
        }

        return (date.Year, date.Day);
    }

    public int GetCurrentPart(int year, int day)
    {
        var puzzle = client.GetPuzzle(year, day);
        return puzzle.Contains("--- Part Two ---") ? 2 : 1;
    }

    public Output Run()
    {
        var (year, day) = GetCurrentDecemberDate();
        var part = GetCurrentPart(year, day);
        return Run(year, day, part);
    }

    public Output Run(int year, int day, int part, string? input = null)
    {
        var type = Type.GetType($"AdventOfCode.Puzzles.Y{year}.Days.Day{day:00}, AdventOfCode.Puzzles.Y{year}", true)!;
        var instance = (Day)type.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
        instance.Input = input ?? inputManager.Get(year, day);
        return part == 1 ? instance.Part1() : instance.Part2();
    }

    public void Print()
    {
        var (year, day) = GetCurrentDecemberDate();
        var part = GetCurrentPart(year, day);
        Print(year, day, part);
    }

    public void Print(int year, int day, int part, string? input = null)
    {
        Console.WriteLine($"Advent of Code {year} day {day} part {part}...");
        Console.WriteLine(Run(year, day, part, input));
    }
}
