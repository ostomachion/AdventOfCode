using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Helpers.Extensions
{
    public static class StringEnumerableExtensions
    {
        public static IEnumerable<HashSet<int>> Find(this IEnumerable<string> text, string needle) => text.Select(x => x.Find(needle));
        public static IEnumerable<HashSet<int>> Find(this IEnumerable<string> text, char needle) => text.Select(x => x.Find(needle));
        public static IEnumerable<int> Count(this IEnumerable<string> text, string needle) => text.Select(x => x.Count(needle));
        public static IEnumerable<int> Count(this IEnumerable<string> text, char needle) => text.Select(x => x.Count(needle));
        public static IEnumerable<HashSet<int>> FindOverlap(this IEnumerable<string> text, string needle) => text.Select(x => x.FindOverlap(needle));
        public static IEnumerable<HashSet<int>> FindOverlap(this IEnumerable<string> text, char needle) => text.Select(x => x.FindOverlap(needle));
        public static IEnumerable<int> CountOverlap(this IEnumerable<string> text, string needle) => text.Select(x => x.CountOverlap(needle));
        public static IEnumerable<int> CountOverlap(this IEnumerable<string> text, char needle) => text.Select(x => x.CountOverlap(needle));

        public static IEnumerable<string[]> Split(this IEnumerable<string> text, char separator, int count, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, count, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, char separator, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, params char[]? separator) => text.Select(x => x.Split(separator));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, char[]? separator, int count) => text.Select(x => x.Split(separator, count));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, char[]? separator, int count, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, count, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, char[]? separator, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, string? separator, int count, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, count, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, string? separator, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, string[]? separator, int count, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, count, options));
        public static IEnumerable<string[]> Split(this IEnumerable<string> text, string[]? separator, StringSplitOptions options = StringSplitOptions.None) => text.Select(x => x.Split(separator, options));
        
        
        public static IEnumerable<int> ParseInt(this IEnumerable<string> text) => text.Select(Int32.Parse);
        public static IEnumerable<int?> TryParseInt(this IEnumerable<string> text) => text.Select(x => Int32.TryParse(x, out int result) ? result : (int?)null);
        public static IEnumerable<int> ParseInt(this IEnumerable<string> text, NumberStyles style) => text.Select(x => Int32.Parse(x, style));
        public static IEnumerable<int?> TryParseInt(this IEnumerable<string> text, NumberStyles style) => text.Select(x => Int32.TryParse(x, style, null, out int result) ? result : (int?)null);

        public static IEnumerable<T?> Parse<T>(this IEnumerable<string> text, params string[] rules) => throw new NotImplementedException();
    }

    public record ParseRule<T>(string Pattern, Func<Match, T?> F);
}