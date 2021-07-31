using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers
{
    public static class EnumerableExtensions
    {
        public static Dictionary<int, TValue> ToDictionary<TValue>(this IEnumerable<TValue> enumerable)
        {
            return enumerable.Select((x, i) => (x, i)).ToDictionary(p => p.i, p => p.x);
        }
    }
}