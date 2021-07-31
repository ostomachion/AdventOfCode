using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers
{
    public static class StringExtensions
    {
        public static HashSet<int> Find(this string text, string needle)
        {
            if (needle.Length == 0)
                return Enumerable.Range(0, text.Length).ToHashSet();

            var value = new HashSet<int>();
            var index = 0;
            while (true)
            {
                index = text.IndexOf(needle, index);
                if (index == -1)
                    break;
                value.Add(index);
                index += needle.Length;
            }
            return value;
        }

        public static HashSet<int> Find(this string text, char needle) => text.Find(needle.ToString());

        public static int Count(this string text, string needle) => text.Find(needle).Count;
        public static int Count(this string text, char needle) => text.Find(needle).Count;

        public static HashSet<int> FindOverlap(this string text, string needle)
        {
            var value = new HashSet<int>();
            var index = 0;
            while (true)
            {
                index = text.IndexOf(needle, index);
                if (index == -1)
                    break;
                value.Add(index);
                index++;
            }
            return value;
        }

        public static HashSet<int> FindOverlap(this string text, char needle) => text.FindOverlap(needle.ToString());

        public static int CountOverlap(this string text, string needle) => text.FindOverlap(needle).Count;
        public static int CountOverlap(this string text, char needle) => text.FindOverlap(needle).Count;
    }
}