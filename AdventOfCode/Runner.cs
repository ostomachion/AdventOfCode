using System.Runtime.CompilerServices;
using System.Reflection;
using System;
using AdventOfCode.Base;
using System.IO;

namespace AdventOfCode
{
    public static class Runner
    {
        private static readonly Client client = new(File.ReadAllText(Path.Combine(InputManager.OutputDir, ".adventofcode.session")));
        private static readonly InputManager inputManager = new(client);

        public static void Run()
        {
            // New puzzles release at midnight Eastern time.
            var nowET = DateTimeOffset.Now.ToOffset(new TimeSpan(-5, 0, 0));
            var year = nowET.Year;
            var day = nowET.Day;
            var part = client.GetPuzzle(year, day).Contains("--- Part Two ---") ? 2 : 1;
            Run(year, day, part);
        }

        public static void Run(int year, int day, int part)
        {
            Console.WriteLine($"Advent of Code {year} day {day} part {part}...");
            
            var type = Type.GetType($"AdventOfCode.Y{year}.Days.Day{day:00}, AdventOfCode.Y{year}", true)!;
            var instance = (Day)type.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
            instance.Input = inputManager.Get(year, day);
            Output output = part == 1 ? instance.Part1() : instance.Part2();
            Console.WriteLine(output);
        }
    }
}