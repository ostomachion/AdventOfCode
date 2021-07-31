using System;

namespace AdventOfCode.Base
{
    public abstract class Day
    {
        public string Input { get; set; }
        
        public abstract Output Part1();
        public abstract Output Part2();
    }
}
