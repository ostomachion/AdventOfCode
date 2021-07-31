using System;

namespace AdventOfCode.Base
{
    public abstract class Day
    {
        public string Input { get; set; } = null!;
        
        public abstract Output Part1();
        public abstract Output Part2();

        public static Output AnswerNotFound() => throw new AnswerNotFoundException();
    }
}
