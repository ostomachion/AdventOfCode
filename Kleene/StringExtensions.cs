﻿using System;
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
            var expression = Expression.Parse(pattern);
            return input.Select(expression.Parse<T>);
        }
    }
}