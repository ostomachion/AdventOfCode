using AdventOfCode.Base;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2015.Days
{
    public class Day01 : Day
    {
        public override Output Part1()
        {
            return Input.Count('(') - Input.Count(')');
        }

        public override Output Part2()
        {
            throw new System.NotImplementedException();
        }
    }
}