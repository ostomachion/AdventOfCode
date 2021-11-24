using System;
using System.Threading.Tasks;

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

    public async Task<int> GetCurrentPartAsync(int year, int day)
    {
        var puzzle = await client.GetPuzzleAsync(year, day);
        return puzzle.Contains("--- Part Two ---") ? 2 : 1;
    }

    public async Task<Output> RunAsync()
    {
        var (year, day) = GetCurrentDecemberDate();
        var part = await GetCurrentPartAsync(year, day);
        return await RunAsync(year, day, part);
    }

    public async Task<Output> RunAsync(int year, int day, int part, string? input = null)
    {
        var type = Type.GetType($"AdventOfCode.Puzzles.Y{year}.Days.Day{day:00}, AdventOfCode.Puzzles.Y{year}", true)!;
        var instance = (Day)type.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
        instance.Input = input ?? await inputManager.GetAsync(year, day);
        return part == 1 ? instance.Part1() : instance.Part2();
    }

    public async Task PrintAsync()
    {
        var (year, day) = GetCurrentDecemberDate();
        var part = await GetCurrentPartAsync(year, day);
        await PrintAsync(year, day, part);
    }

    public async Task PrintAsync(int year, int day, int part, string? input = null)
    {
        Console.WriteLine($"Advent of Code {year} day {day} part {part}...");
        Console.WriteLine(await RunAsync(year, day, part, input));
    }
}
