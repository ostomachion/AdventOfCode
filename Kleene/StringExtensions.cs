using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kleene
{
    public static class StringExtensions
    {
        public static T? Parse<T>(this string input, string pattern)
        {
            return Expression.Parse(pattern).Parse<T>(input);
        }

        public static IEnumerable<T?> Parse<T>(this IEnumerable<string> input, string pattern)
        {
            return input.Select(x => Expression.Parse(pattern).Parse<T>(x));
        }
    }
}
